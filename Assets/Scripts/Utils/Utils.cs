
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class Utils
    {
        public static List<GameObject> GetClosestObject(GameObject around, float distance, String tag)
        {
            List<GameObject> found = new List<GameObject>();
            GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject obj in objectsWithTag)
            {
                if (!obj.Equals(around))
                {
                    //compares distances
                    if (Vector3.Distance(around.transform.position, obj.transform.position) <= distance)
                    {
                        found.Add(obj);
                    }
                }
            }
            return found;
        }
        
    }
