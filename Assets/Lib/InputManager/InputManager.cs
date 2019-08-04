using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    InputHandler inputHandler;
    Entity myEntity;

    private void Awake()
    {
        myEntity = GetComponent<Entity>();
        inputHandler = new InputHandler(myEntity); 
    }

    private void Update()
    {
        Command toExecute = inputHandler.HandleInput();
        toExecute.Execute();
    }

    public class InputHandler
    {
        Command keyW;
        Command keyA;
        Command keyS;
        Command keyD;
        Command mouse0Down;
        Command doNothing;
        Dictionary<KeyCode, Command> keyMap = new Dictionary<KeyCode, Command>();
        public InputHandler(Entity entity)
        {
            keyW = new MoveUp(entity);
            keyA = new MoveLeft(entity);
            keyS = new MoveDown(entity);
            keyD = new MoveRight(entity);
            mouse0Down = new MoveToMouse(entity);
            doNothing = new DoNothing(entity);

            keyMap.Add(KeyCode.W, new MoveUp(entity));
            keyMap.Add(KeyCode.A, new MoveLeft(entity));
            keyMap.Add(KeyCode.S, new MoveDown(entity));
            keyMap.Add(KeyCode.D, new MoveRight(entity));
        }

        public void Remap(KeyCode key, Command command)
        {
             //TODO - create a dictionary of key -> commands so that we can keep track/remap things easily.
             // TODO - if key assigned, unassign and remap.
             // TODO - on remap/add key, also add listener to gameinputmanager so that we can listen for events instead of looking at all possible keys etc.
        }

        public Command HandleInput()
        {
            if (Input.GetKey(KeyCode.W))
            {
                return keyW;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                return keyA;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                return keyS;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                return keyD;
            }
            else if (Input.GetMouseButton(0))
            {
                return mouse0Down;
            }
            else
            {
                return doNothing;
            }
        }
    }


}
