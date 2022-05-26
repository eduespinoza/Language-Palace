//-----------------------------------------------------------------------
// <copyright file="CameraPointer.cs" company="Google LLC">
// Copyright 2020 Google LLC
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections;
using UnityEngine;

/// <summary>
/// Sends messages to gazed GameObject.
/// </summary>
public class CameraPointer : MonoBehaviour
{
    static CameraPointer instance = null;
    public static CameraPointer Instance {
        get {
            if (instance == null) {
                instance = FindObjectOfType<CameraPointer>();
            }
            return instance;
        }
    }

    public float maxDistance = 10;
    Actionable action = null;
    // private GameObject _gazedAtObject = null;


    void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    public void Update()
    {
        // Casts ray towards camera's forward direction, to detect if a GameObject is being gazed
        // at.
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, maxDistance))
        {
            // GameObject detected in front of the camera.
            Actionable action = hit.transform.GetComponent<Actionable>();
            if (action != null && action != this.action) {
                    this.action?.PointerExit();
                    this.action = action;
                    action.PointerEnter();
                }

                if (action == null) {
                    this.action?.PointerExit();
                    this.action = null;
                }
        }
        else
        {
            // No GameObject detected in front of the camera.
            action?.PointerExit();
            action = null;
        }

        // Checks for screen touches.
        if (Google.XR.Cardboard.Api.IsTriggerPressed)
        {
            action.PointerDown();
        }
    }
}
