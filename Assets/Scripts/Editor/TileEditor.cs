using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System;
using System.IO;

[CustomEditor(typeof(Tile))]
[CanEditMultipleObjects]
[ExecuteInEditMode]
public class TileEditor : Editor
{
    List<GameObject> Tiles;
    string TileType;
    public static int amount;
    GameObject prevTile;
    bool LastActionWasCreate = false;
    int index = 0;
    public override void OnInspectorGUI()
    {

        DrawDefaultInspector();

        Event e = Event.current;
        Tile myTile = (Tile)target;

        //int amountToMake = EditorGUILayout.IntField("Amount Of Tiles:", amount);
        Rect r = EditorGUILayout.BeginHorizontal();
        string key = "";
        /*switch (e.type)
        {
            case EventType.keyDown:
                {
                    if (Event.current.keyCode == (KeyCode.A))
                    {
                        GameObject newTile = Instantiate(myTile.BaseTile.gameObject, new Vector3(myTile.gameObject.transform.position.x - myTile.gameObject.transform.localScale.x, myTile.gameObject.transform.position.y, myTile.gameObject.transform.position.z), Quaternion.identity) as GameObject;
                        Selection.activeGameObject = newTile;
                        newTile.name = myTile.name;
                        LastActionWasCreate = true;
                    }

                    else if (Event.current.keyCode == (KeyCode.W))
                    {
                        key = "w";
                    }
                    else if (Event.current.keyCode == (KeyCode.S))
                    {
                        key = "s";
                    }
                    else if (Event.current.keyCode == (KeyCode.D))
                    {
                        key = "d";
                    }
                    break;
                }
        }*/

        List<string> names = new List<string>();
        List<string> options=new List<string>();
        if (Tile.TileData == null) { Tile.TileData = new Dictionary<string, TileData>();BlocksFromJSON(); }
        foreach (string k in Tile.TileData.Keys) {
            options.Add(k);
            if (k == myTile.name)
            {
                index = options.Count - 1;
            }
        }
        string[] Options = options.ToArray();
        int prevIndex = index;
        GUILayout.Label("Tile Type:");
        if (options.Count > 0)
        {
            index = EditorGUILayout.Popup(index, Options);
        }
        if (options.Count > 0&&prevIndex!=index)
        {
            ChangeTile(myTile, options[index]);
        }
        if (GUILayout.Button("Add Tile")) {
            AddTile(myTile);
        }
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("<"))
        {
            if (myTile.currentMode == Tile.Mode.Create)
            {
                MakeMyTile("W", myTile, myTile.amount);
            }
            else if (myTile.currentMode == Tile.Mode.Move)
            {
                MoveMyTile("W", myTile, myTile.amount);
            }
            // Tiles.Add(newTile);

        }
        if (GUILayout.Button("V"))
        {
            if (myTile.currentMode == Tile.Mode.Create)
            {
                MakeMyTile("S", myTile, myTile.amount);
            }
            else if (myTile.currentMode == Tile.Mode.Move)
            {
                MoveMyTile("S", myTile, myTile.amount);
            }
            //  Tiles.Add(newTile);
        }
        if (GUILayout.Button("^"))
        {
            if (myTile.currentMode == Tile.Mode.Create)
            {
                MakeMyTile("N", myTile, myTile.amount);
            }
            else if (myTile.currentMode == Tile.Mode.Move)
            {
                MoveMyTile("N", myTile, myTile.amount);
            }
        }
        if (GUILayout.Button(">"))
        {
            if (myTile.currentMode == Tile.Mode.Create)
            {
                MakeMyTile("E", myTile, myTile.amount);
            }
            else if (myTile.currentMode == Tile.Mode.Move)
            {
                MoveMyTile("E", myTile, myTile.amount);
            }
        }
        EditorGUILayout.EndHorizontal();
        if (GUILayout.Button("Delete"))
        {
            //  Tiles.Remove(myTile.gameObject);
            DestroyImmediate(myTile.gameObject);
            Selection.activeGameObject = prevTile;
        }
        //public Texture tex;
        // Texture tex;
        //myTile.gameObject.GetComponent<Renderer>().material.mainTexture= EditorGUILayout.ObjectField("Label:", target.tex, typeof(Texture), true); ;
    }

    public void MakeMyTile(string direction, Tile selectedTile, int howMany)
    {
        GameObject Parent = GameObject.Find("Tiles");
        GameObject container = null; ;
        if (howMany > 1)
        {
            container = new GameObject("Container");
            container.transform.position = selectedTile.transform.position;
            container.transform.parent = selectedTile.transform.parent;
            selectedTile.transform.parent = Parent.transform;
            selectedTile.Parent = container;
        }
        for (int i = 0; i < howMany; i++)
        {
            GameObject newTile;
            prevTile = selectedTile.gameObject;
            Transform t = selectedTile.gameObject.transform;
            float height = t.position.y;

            switch (direction)
            {
                case "W":
                    newTile = Instantiate(selectedTile.BaseTile.gameObject,
                        new Vector3(t.position.x - t.localScale.x, t.position.y), Quaternion.identity) as GameObject;
                    break;
                case "N":
                    newTile = Instantiate(selectedTile.BaseTile.gameObject, new Vector3(t.position.x, t.position.y + t.localScale.y), Quaternion.identity) as GameObject;
                    break;
                case "S":
                    newTile = Instantiate(selectedTile.BaseTile.gameObject, new Vector3(t.position.x, t.position.y - t.localScale.y), Quaternion.identity) as GameObject;
                    break;
                case "E":
                    newTile = Instantiate(selectedTile.BaseTile.gameObject, new Vector3(t.position.x + t.localScale.x, t.position.y), Quaternion.identity) as GameObject;
                    break;
                default:
                    newTile = Instantiate(selectedTile.BaseTile.gameObject, new Vector3(t.position.x + t.localScale.x, t.position.y), Quaternion.identity) as GameObject;
                    break;
            }

            newTile.transform.parent = Parent.transform;
            Selection.activeGameObject = newTile;
            newTile.name = selectedTile.name;
            LastActionWasCreate = true;
            Tile prev = selectedTile;
            selectedTile = newTile.GetComponent<Tile>();
            selectedTile.amount = 1;
            selectedTile.Parent = prev.Parent;
            selectedTile.currentMode = Tile.Mode.Create;
            selectedTile.transform.parent = prev.transform.parent;
            // selectedTile.tag = selectedTile.tag; ;
        }
    }
    public void MoveMyTile(string direction, Tile selectedTile, int howMany)
    {
        switch (direction)
        {
            case "E": selectedTile.transform.position = new Vector3(selectedTile.transform.position.x + howMany, selectedTile.transform.position.y); break;
            case "N": selectedTile.transform.position = new Vector3(selectedTile.transform.position.x, selectedTile.transform.position.y + howMany); break;
            case "S": selectedTile.transform.position = new Vector3(selectedTile.transform.position.x, selectedTile.transform.position.y - howMany); break;
            case "W": selectedTile.transform.position = new Vector3(selectedTile.transform.position.x - howMany, selectedTile.transform.position.y); break;
        }
    }
    /* public void makeSquare(int x, int y, Tile selectedTile) {
         string dir="E";
         for () {
             MakeMyTile(dir,selectedTile,x);
         }*/

        public int NumericKey(KeyCode k)
    {
        if (k == KeyCode.Alpha0){ return 0; }
        if (k == KeyCode.Alpha1) { return 1; }
        if (k == KeyCode.Alpha2) { return 2; }
        if (k == KeyCode.Alpha3) { return 3; }
        if (k == KeyCode.Alpha4) { return 4; }
        if (k == KeyCode.Alpha5) { return 5; }
        if (k == KeyCode.Alpha6) { return 6; }
        if (k == KeyCode.Alpha7) { return 7; }
        if (k == KeyCode.Alpha8) { return 8; }
        if (k == KeyCode.Alpha9) { return 9; }
        return -1;
    }
    void OnSceneGUI()
    {
        Event e = Event.current;
        Tile myTile = (Tile)target;
        Camera camera = Camera.main;
        //Draw what can be seen around tile
       /* if (!Application.isPlaying) {
            Vector3 p = camera.ViewportToWorldPoint(new Vector3(camera.orthographicSize / 7, camera.aspect * 1.25f, 0));
            Handles.DrawWireCube(myTile.transform.position, p);//I'll figure out something better then magic numbers later
        }*/
        switch (e.type)
        {
            case EventType.KeyDown:
                {

                    if (Event.current.keyCode == (KeyCode.A))
                    {
                        if (myTile.currentMode == Tile.Mode.Create)
                        {
                            MakeMyTile("W", myTile, myTile.amount);
                        }
                        else if (myTile.currentMode == Tile.Mode.Move)
                        {
                            MoveMyTile("W", myTile, myTile.amount);
                        }
                        // Tiles.Add(newTile);

                    }
                    if (Event.current.keyCode == (KeyCode.S))
                    {
                        if (myTile.currentMode == Tile.Mode.Create)
                        {
                            MakeMyTile("S", myTile, myTile.amount);
                        }
                        else if (myTile.currentMode == Tile.Mode.Move)
                        {
                            MoveMyTile("S", myTile, myTile.amount);
                        }
                        //  Tiles.Add(newTile);
                    }
                    if (Event.current.keyCode == (KeyCode.W))
                    {
                        if (myTile.currentMode == Tile.Mode.Create)
                        {
                            MakeMyTile("N", myTile, myTile.amount);
                        }
                        else if (myTile.currentMode == Tile.Mode.Move)
                        {
                            MoveMyTile("N", myTile, myTile.amount);
                        }
                    }
                    if (Event.current.keyCode == (KeyCode.D))
                    {
                        if (myTile.currentMode == Tile.Mode.Create)
                        {
                            MakeMyTile("E", myTile, myTile.amount);
                        }
                        else if (myTile.currentMode == Tile.Mode.Move)
                        {
                            MoveMyTile("E", myTile, myTile.amount);
                        }
                    }
                    if (Event.current.keyCode == (KeyCode.V))
                    {
                        myTile.currentMode++;
                        if ((int)myTile.currentMode == Enum.GetValues(typeof(Tile.Mode)).Length) { myTile.currentMode = 0; }
                    }
                    if (Event.current.keyCode == (KeyCode.C))
                    {
                        myTile.currentMode--;
                        if ((int)myTile.currentMode == -1) { myTile.currentMode = (Tile.Mode)Enum.GetValues(typeof(Tile.Mode)).Length - 1; }
                    }
                    if (Event.current.keyCode == (KeyCode.X))
                    {
                        Debug.Log("X");
                        if (index < Tile.TileData.Count-1)
                        {
                            index++;
                        }
                        else { index = 0; }
                        Debug.Log(index);
                        
                    }
                    if (Event.current.keyCode == (KeyCode.Z))
                    {
                        if (index >0)
                        {
                            index--;
                        }
                        else { index = Tile.TileData.Count-1; }
                    }
                    if (Event.current.keyCode == (KeyCode.Space))
                    {
                        DestroyImmediate(myTile.gameObject);
                        Selection.activeGameObject = prevTile;
                    }
                    if (NumericKey( Event.current.keyCode)>-1)
                    {
                        myTile.amount = NumericKey(Event.current.keyCode);
                    }
                    break;
                }
        }
    }

    void AddTile(Tile t)
    {
        TileData td = new TileData();
        td.Name = t.name;
        td.s = t.gameObject.GetComponent<SpriteRenderer>().sprite;
        td.Tag = t.tag;
        td.Scripts= t.gameObject.GetComponents<MonoBehaviour>();
        if (Tile.TileData.ContainsKey(td.Name)) { Tile.TileData[td.Name] = td; }
        else { Tile.TileData.Add(td.Name, td); }
        BlocksToJSON();
    }
    void ChangeTile(Tile t,string TileName)
    {
        BlocksFromJSON();
        if (Tile.TileData.ContainsKey(TileName))
        {
            TileData td = Tile.TileData[TileName];
            t.name = td.Name;
            t.gameObject.GetComponent<SpriteRenderer>().sprite = td.s;
            t.tag = td.Tag;
            foreach(MonoBehaviour mb in t.gameObject.GetComponents<MonoBehaviour>())
            {
                if (mb.GetType()!=typeof(Tile)) {
                    DestroyImmediate(mb);
                }
            }
            foreach (MonoBehaviour mb in td.Scripts)
            {
                try{
                    if (mb == null) { Debug.Log("mb is null, but script works"); }
                    if (mb.GetType() != typeof(Tile))
                    {
                        Type T = mb.GetType();
                        t.gameObject.AddComponent(mb.GetType());
                    }
                }
                catch
                {

                }
                
            }
            //TODO: Fix saving sprites
             //   Resources.Load("Assets/Resources/Textures/Turner.png");
        }
    }
    void RemoveTileFromDictionary(Tile t)
    {
        if (Tile.TileData.ContainsKey(t.name))
        {
            Tile.TileData.Remove(t.name);
        }
    }

    void BlocksToJSON()
    {
        Dictionary<string,TileData> temp= Tile.TileData;
        foreach (TileData t in Tile.TileData.Values)
        {
            string json = JsonUtility.ToJson(t, true);
            string FilePath;
            try
            {
                FilePath = Application.dataPath + "/Data/Tiles/" + t.Name + ".json";
            }
            catch (Exception e)
            {
                Debug.Log("File not found so it was created");
                File.Create(Application.dataPath + "/Data/Tiles/" + t.Name + ".json");
                FilePath = Application.dataPath + "/Data/Tiles/" + t.Name + ".json";
            }
            File.WriteAllText(FilePath, json);
        }
    }
    void BlocksFromJSON()
    {
        string directory = Application.dataPath + "/Data/Tiles/";
        string[] Files = Directory.GetFiles(directory);
        foreach(string FileName in Files)
        {
            string TileName = FileName.Split('/')[0];
            if (FileName.EndsWith(".json"))
            {
                string json = File.ReadAllText(FileName);
                TileData td = new TileData();
                td = JsonUtility.FromJson<TileData>(json);
                if (Tile.TileData == null) { Tile.TileData = new Dictionary<string, TileData>(); }
                if (!Tile.TileData.ContainsKey(td.Name)) { 
                Tile.TileData.Add(td.Name, td);
                }
            }
        }
    }
}