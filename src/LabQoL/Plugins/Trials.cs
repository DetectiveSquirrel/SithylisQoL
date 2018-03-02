using ImGuiNET;
using PoeHUD.Poe.Components;
using Random_Features.Libs;
using ImVector4 = System.Numerics.Vector4;

namespace Random_Features
{
    partial class RandomFeatures
    {
        public void TrialMenu()
        {
            ImGui.Text("Normal Trials");
            ImGui.Separator();
            ImGui.Columns(2, "normalTrials", true);
            for (var i = 0; i < 6; i++)
            {
                if (i == 3)
                    ImGui.NextColumn();
                TrialString(i);
            }

            ImGuiExtension.EndColumn();
            ImGui.Separator();
            ImGuiExtension.Spacing(4);
            ImGui.Text("Cruel Trials");
            ImGui.Separator();
            for (var i = 6; i < 9; i++)
                TrialString(i);
            ImGui.Separator();
            ImGuiExtension.Spacing(4);
            ImGui.Text("Merciless Trials");
            ImGui.Separator();
            for (var i = 9; i < 12; i++)
                TrialString(i);
            ImGui.Separator();
            ImGuiExtension.Spacing(4);
            ImGui.Text("Uber Trials");
            ImGui.Separator();
            ImGui.Columns(2, "uberTrials", true);
            for (var i = 12; i < 18; i++)
            {
                if (i == 15)
                    ImGui.NextColumn();
                TrialString(i);
            }

            ImGui.Separator();
            ImGuiExtension.EndColumn();
        }

        public void TrialString(int trialID)
        {
            var trial = GameController.Player.GetComponent<Player>().TrialStates[trialID];
            if (trial.IsCompleted)
                ImGui.PushStyleColor(ColorTarget.Text, new ImVector4(0.25f, 0.85f, 0.25f, 1f));
            else
                ImGui.PushStyleColor(ColorTarget.Text, new ImVector4(0.85f, 0.25f, 0.25f, 1f));
            if (trial.TrialArea.Area.Act <= 10)
                ImGui.Text($"{trial.TrialArea.Area.Name} (Act {trial.TrialArea.Area.Act})");
            else
                ImGui.Text($"{trial.TrialArea.Area.Name} (Maps)");
            ImGui.PopStyleColor(1);
        }
    }
}