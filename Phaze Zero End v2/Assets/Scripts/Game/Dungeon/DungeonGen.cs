using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class DungeonGen : MonoBehaviour
{
    [FormerlySerializedAs("Rooms")] public RoomModule[] rooms;
    [FormerlySerializedAs("StartRoom")] public RoomModule startRoom;
    [FormerlySerializedAs("BossRoom")] public RoomModule bossRoom;
    [FormerlySerializedAs("DeadEnd")] public RoomModule deadEnd;
    [FormerlySerializedAs("SecretRoom")] public RoomModule secretRoom;

    public int Generations;

    private void Start()
    {
        var start = (RoomModule)Instantiate(startRoom, transform.position, transform.rotation);
        var pendExits = new List<RoomConnector>(start.GetExitsForRoom());
        bool BossRoomSpawned = false;
        bool SecretRoomSpawned = false;

        for (int gens = 0; gens <= Generations; gens++)
        {
            var newExit = new List<RoomConnector>();
            string newTag;
            RoomModule newRoomPrefab;
            RoomModule newRoom;
            RoomConnector[] newRoomExits;
            RoomConnector exitMatch;

            foreach (var pendExit in pendExits)
            {
                if (gens == Generations && pendExits.Count > 0 && !BossRoomSpawned) //Checks for spawning Boss Room
                {
                    newRoom = (RoomModule)Instantiate(bossRoom);
                    newRoomExits = newRoom.GetExitsForRoom();
                    exitMatch = newRoomExits.FirstOrDefault(x => x.IsDefault) ?? GetRandom(newRoomExits);
                    MatchExit(pendExit, exitMatch);
                    BossRoomSpawned = true;
                }
                else if (gens == Generations && pendExits.Count > 0 && BossRoomSpawned && !SecretRoomSpawned) //Checks for spawning Secret Room
                {
                    newRoom = (RoomModule)Instantiate(secretRoom);
                    newRoomExits = newRoom.GetExitsForRoom();
                    exitMatch = newRoomExits.FirstOrDefault(x => x.IsDefault) ?? GetRandom(newRoomExits);
                    MatchExit(pendExit, exitMatch);
                    SecretRoomSpawned = true;
                }
                else if (gens == Generations && pendExits.Count > 0 && BossRoomSpawned && SecretRoomSpawned) //Checks for remaining exits to fill with dead ends
                {
                    newRoom = (RoomModule)Instantiate(deadEnd);
                    newRoomExits = newRoom.GetExitsForRoom();
                    exitMatch = newRoomExits.FirstOrDefault(x => x.IsDefault) ?? GetRandom(newRoomExits);
                    MatchExit(pendExit, exitMatch);
                }
                else //Selects random room from list
                {
                    newTag = GetRandom(pendExit.connectorTags);
                    newRoomPrefab = GetRandomRoom(rooms, newTag);

                    newRoom = (RoomModule)Instantiate(newRoomPrefab);
                    newRoomExits = newRoom.GetExitsForRoom();
                    exitMatch = newRoomExits.FirstOrDefault(x => x.IsDefault) ?? GetRandom(newRoomExits);
                    MatchExit(pendExit, exitMatch);
                    newExit.AddRange(newRoomExits.Where(e => e != exitMatch));
                }
            }

            pendExits = newExit;

        }

    }

    private void MatchExit(RoomConnector oldExit, RoomConnector newExit)
    {
        var newRoom = newExit.transform.parent;
        var forwardVectToMatch = -oldExit.transform.forward;
        var correctRotate = Azimuth(forwardVectToMatch) - Azimuth(newExit.transform.forward);
        newRoom.RotateAround(newExit.transform.position, Vector3.up, correctRotate);
        var correctTranslate = oldExit.transform.position - newExit.transform.position;
        newRoom.transform.position += correctTranslate;
    }

    private static TItem GetRandom<TItem>(TItem[] array)
    {
        return array[Random.Range(0, array.Length)];
    }

    private static RoomModule GetRandomRoom(IEnumerable<RoomModule> rooms, string tagMatch)
    {
        var matchingRooms = rooms.Where(r => r.RoomTags.Contains(tagMatch)).ToArray();
        return GetRandom(matchingRooms);
    }

    private static float Azimuth(Vector3 vector)
    {
        return Vector3.Angle(Vector3.forward, vector) * Mathf.Sign(vector.x);
    }
}
