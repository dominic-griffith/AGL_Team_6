using ImGuiNET;
using UnityEngine;

public class GUIDebug : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField]
    private UImGui.UImGui uimGuiInstance;
    [SerializeField] 
    private InputMovementController inputMovementController;
    [SerializeField] 
    private MovementSystem movementSystem;
    [SerializeField] 
    private MovementSettings movementSettings;
    [SerializeField] 
    private Controls controls;
    [SerializeField] 
    private AbilitySystem abilitySystem;

    [Header("Settings")]
    [SerializeField]
    private bool isPlayerDebugOpen;
    
    private void Awake()
    {
        if (uimGuiInstance == null)
        {
            Debug.LogError("Must assign a UImGuiInstance or use UImGuiUtility with Do Global Events on UImGui component.");
        }

        uimGuiInstance.Layout += OnLayout;
        uimGuiInstance.OnInitialize += OnInitialize;
        uimGuiInstance.OnDeinitialize += OnDeinitialize;
    }

    private void OnLayout(UImGui.UImGui obj)
    {
        if (isPlayerDebugOpen)
        {
            ImGui.Begin("Input System Debug",ref isPlayerDebugOpen);
            ImGui.Text("Input System axis");
            ImGui.SliderFloat("forward axis", ref inputMovementController.forwardAxis, -1f, 1.0f);
            ImGui.SliderFloat("right axis", ref inputMovementController.rightAxis, -1f, 1.0f);
            Vector3 test = movementSystem.MovementDirection;
            ImGui.DragFloat3("All Axis normalized", ref test);
            float test2 = movementSystem.MovementDirection.magnitude;
            ImGui.DragFloat("Magnitude", ref test2);
            Vector3 test3 = movementSettings.playerVelocity;
            ImGui.DragFloat3("Speed applied", ref test3);
            ImGui.SliderFloat("Speed applied", ref movementSettings.movementSpeed, 0f, 30f);
        
            ImGui.Text("Input System keys");
            bool inputForward = Input.GetKey(controls.forward) ;
            ImGui.Checkbox("W pressed", ref inputForward);
            bool inputLeft = Input.GetKey(controls.left) ;
            ImGui.Checkbox("A pressed", ref inputLeft);
            bool inputBackward = Input.GetKey(controls.backward) ;
            ImGui.Checkbox("S pressed", ref inputBackward);
            bool inputRight = Input.GetKey(controls.right) ;
            ImGui.Checkbox("D pressed", ref inputRight);
            
            ImGui.End();
        }
        

    }

    private void OnInitialize(UImGui.UImGui obj)
    {
        // runs after UImGui.OnEnable();
    }

    private void OnDeinitialize(UImGui.UImGui obj)
    {
        // runs after UImGui.OnDisable();
    }

    private void OnDisable()
    {
        uimGuiInstance.Layout -= OnLayout;
        uimGuiInstance.OnInitialize -= OnInitialize;
        uimGuiInstance.OnDeinitialize -= OnDeinitialize;
    }
}