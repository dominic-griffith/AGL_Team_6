using System;
using System.Globalization;
using ImGuiNET;
using UnityEngine;
using ImPlotNET;

public class GUIDebug : MonoBehaviour
{
    [Header("Dependencies")] [SerializeField]
    private UImGui.UImGui uimGuiInstance;

    [SerializeField] private InputMovementController inputMovementController;
    [SerializeField] private MovementSystem movementSystem;
    [SerializeField] private MovementSettings movementSettings;
    [SerializeField] private Controls controls;
    [SerializeField] private AbilitySystem abilitySystem;

    [Header("Settings")] [SerializeField] private bool isPlayerDebugOpen;
    
    private bool animate = true;
    private float progress_saturated = 0f;
    private float progress = 0.0f;
    private float progress_dir = 1.0f;

    private void Awake()
    {
        if (uimGuiInstance == null)
        {
            Debug.LogError(
                "Must assign a UImGuiInstance or use UImGuiUtility with Do Global Events on UImGui component.");
        }

        uimGuiInstance.Layout += OnLayout;
        uimGuiInstance.OnInitialize += OnInitialize;
        uimGuiInstance.OnDeinitialize += OnDeinitialize;
    }

    private void OnLayout(UImGui.UImGui obj)
    {
        if (isPlayerDebugOpen)
        {
            ImGui.Begin("Input System Debug", ref isPlayerDebugOpen);
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
            bool inputForward = Input.GetKey(controls.forward);
            ImGui.Checkbox("W pressed", ref inputForward);
            bool inputLeft = Input.GetKey(controls.left);
            ImGui.Checkbox("A pressed", ref inputLeft);
            bool inputBackward = Input.GetKey(controls.backward);
            ImGui.Checkbox("S pressed", ref inputBackward);
            bool inputRight = Input.GetKey(controls.right);
            ImGui.Checkbox("D pressed", ref inputRight);

            ImGui.End();

            if (ImGui.TreeNode("Plotting"))
            {
                ImGui.Checkbox("Animate", ref animate);

                // Plot as lines and plot as histogram
                float[] arr = {0.6f, 0.1f, 1.0f, 0.5f, 0.92f, 0.1f, 0.2f};
                ImGui.PlotLines("Frame Times", ref arr[0], arr.Length);
                ImGui.PlotHistogram("Histogram", ref arr[0], arr.Length, 0, null, 0.0f, 1.0f, new Vector2(0, 80.0f));
                // Fill an array of contiguous float values to plot
                // Tip: If your float aren't contiguous but part of a structure, you can pass a pointer to your first float
                // and the sizeof() of your structure in the "stride" parameter.
                float[] values = new float[90];
                int values_offset = 0;
                double refresh_time = 0.0;
                if ( !animate )
                    refresh_time = ImGui.GetTime();
                float phase = 0.0f;
                while (refresh_time < ImGui.GetTime()) // Create data at fixed 60 Hz rate for the demo
                {
                    values[values_offset] = MathF.Cos(phase);
                    values_offset = (values_offset + 1) % values.Length;
                    phase += 0.10f * values_offset;
                    refresh_time += 1.0f / 60.0f;
                }

                // Plots can display overlay texts
                // (in this example, we will display an average value)
                float average = 0.0f;
                for (int n = 0; n < values.Length; n++)
                    average += values[n];
                average /= (float) values.Length;
                string overlay = average.ToString();
                ImGui.PlotLines("Lines", ref values[0], values.Length, values_offset, overlay, -1.0f, 1.0f,
                    new Vector2(0, 80.0f));

                // Animate a simple progress bar
                if (animate)
                {
                    progress += progress_dir * 0.4f * ImGui.GetIO().DeltaTime;
                    if (progress >= +1.1f) { progress = +1.1f; progress_dir *= -1.0f; }
                    if (progress <= -0.1f) { progress = -0.1f; progress_dir *= -1.0f; }
                }
                ImGui.ProgressBar(progress, new Vector2(0.0f, 0.0f));
                ImGui.SameLine(0.0f, ImGui.GetStyle().ItemInnerSpacing.x);
                ImGui.Text("Progress Bar");
    
                /*progress = Mathf.Clamp01(progress);
                string buf = ((int)(progress_saturated * 1753)/1753).ToString();
                //sprintf(buf, "%d/%d", (int)(progress_saturated * 1753), 1753);
                ImGui.ProgressBar(progress, new Vector2(0f, 0f), buf);*/
                
                ImGui.TreePop();
            }
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