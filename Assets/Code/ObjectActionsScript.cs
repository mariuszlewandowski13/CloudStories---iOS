using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectActionType
{
    animation,
    changeScene,
    URL,
    audio
}

public class ObjectAction
{
    public bool updated;
    public ObjectActionType type;
    public string additionalData;

    public ObjectAction(ObjectActionType newType, string addData)
    {
        type = newType;
        updated = true;
        additionalData = addData;
    }
}

public class ObjectActionsScript : MonoBehaviour {

    private List<ObjectAction> actions;
    private List<ObjectAction> actionsToRemove;

    public string objectID;

    private void Start()
    {
        
    }

    public void ProcessLines(string[] line, string id)
    {
        objectID = id;
        ClearProjectsUpdate();
        for (int i = 0; i < line.Length; i += 2)
        {
            CheckActionExistsAndProcess((ObjectActionType)(int.Parse(line[i])), line[i + 1]);
        }
        RemoveNonExistingActions();

    }

    private void ClearProjectsUpdate()
    {
        if (actions == null)
        {
            actions = new List<ObjectAction>();
            return;

        }
        foreach (ObjectAction act in actions)
        {
            act.updated = false;
        }
    }

    private void RemoveNonExistingActions()
    {
        actionsToRemove = new List<ObjectAction>();
        foreach (ObjectAction act in actions)
        {
            if (act.updated == false)
            {
                RemoveActions(act);
                actionsToRemove.Add(act);
            }
        }

        foreach (ObjectAction act in actionsToRemove)
        {
            actions.Remove(act);
        }

        actionsToRemove.Clear();
    }

    private void CheckActionExistsAndProcess(ObjectActionType type, string data)
    {
        foreach (ObjectAction act in actions)
        {
            if (act.type == type)
            {
                act.updated = true;
                if (act.additionalData != data)
                {
                    act.additionalData = data;
                    UpdateActions(act);
                }
                return;
            }
        }

        ObjectAction act2 = new ObjectAction(type, data);
        actions.Add(act2);
        UpdateActions(act2);
    }

    private void UpdateActions(ObjectAction act)
    {
        switch (act.type)
        {
            case ObjectActionType.audio:
                UpdateAudio(act);
                break;
            case ObjectActionType.animation:
                UpdateAnimation(act);
                break;
            case ObjectActionType.changeScene:
                UpdateSceneChange(act);
                break;
            case ObjectActionType.URL:
                UpdateURL(act);
                break;
        }
    }

    private void UpdateAudio(ObjectAction act)
    {
        if (GetComponent<AudioActionScript>() == null) gameObject.AddComponent<AudioActionScript>();
        GetComponent<AudioActionScript>().SetActionData(act.additionalData);
    }

    private void UpdateSceneChange(ObjectAction act)
    {
        if (GetComponent<SceneChangeActionScript>() == null) gameObject.AddComponent<SceneChangeActionScript>();
        GetComponent<SceneChangeActionScript>().SetActionData(act.additionalData);
    }

    private void UpdateAnimation(ObjectAction act)
    {
        if (GetComponent<AnimatioNActionScript>() == null) gameObject.AddComponent<AnimatioNActionScript>();
        GetComponent<AnimatioNActionScript>().SetActionData(act.additionalData);
    }

    private void UpdateURL(ObjectAction act)
    {
        if (GetComponent<URLActionScript>() == null) gameObject.AddComponent<URLActionScript>();
        GetComponent<URLActionScript>().SetActionData(act.additionalData);
    }

    private void RemoveActions(ObjectAction act)
    {
        switch (act.type)
        {
            case ObjectActionType.audio:
                RemoveAudio(act);
                break;
            case ObjectActionType.animation:
                RemoveAnimation(act);
                break;
            case ObjectActionType.changeScene:
                RemoveSceneChange(act);
                break;
            case ObjectActionType.URL:
                RemoveURL(act);
                break;
        }
    }

    private void RemoveAudio(ObjectAction act)
    {
        if (GetComponent<AudioActionScript>() != null) Destroy(GetComponent<AudioActionScript>());
    }

    private void RemoveSceneChange(ObjectAction act)
    {
        if (GetComponent<SceneChangeActionScript>() != null) Destroy(GetComponent<SceneChangeActionScript>());
    }

    private void RemoveAnimation(ObjectAction act)
    {
        if (GetComponent<AnimatioNActionScript>() != null) Destroy(GetComponent<AnimatioNActionScript>());
    }

    private void RemoveURL(ObjectAction act)
    {
        if (GetComponent<URLActionScript>() != null) Destroy(GetComponent<URLActionScript>());
    }

}
