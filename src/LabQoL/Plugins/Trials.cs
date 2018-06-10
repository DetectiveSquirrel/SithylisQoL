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
            for (var I = 0; I < 6; I++)
            {
                if (I == 3)
                    ImGui.NextColumn();
                TrialString(I);
            }

            ImGuiExtension.EndColumn();
            ImGui.Separator();
            ImGuiExtension.Spacing(4);
            ImGui.Text("Cruel Trials");
            ImGui.Separator();
            for (var I = 6; I < 9; I++)
                TrialString(I);
            ImGui.Separator();
            ImGuiExtension.Spacing(4);
            ImGui.Text("Merciless Trials");
            ImGui.Separator();
            for (var I = 9; I < 12; I++)
                TrialString(I);
            ImGui.Separator();
            ImGuiExtension.Spacing(4);
            ImGui.Text("Uber Trials");
            ImGui.Separator();
            ImGui.Columns(2, "uberTrials", true);
            for (var I = 12; I < 18; I++)
            {
                if (I == 15)
                    ImGui.NextColumn();
                TrialString(I);
            }

            ImGui.Separator();
            ImGuiExtension.EndColumn();
            Settings.Debug = ImGuiExtension.Checkbox("Debug", Settings.Debug);
        }

        public void TrialString(int TrialId)
        {
            var Trial = GameController.Player.GetComponent<Player>().TrialStates[TrialId];
            if (Trial.IsCompleted)
                ImGui.PushStyleColor(ColorTarget.Text, new ImVector4(0.25f, 0.85f, 0.25f, 1f));
            else
                ImGui.PushStyleColor(ColorTarget.Text, new ImVector4(0.85f, 0.25f, 0.25f, 1f));
            if (Trial.TrialArea.Area.Act <= 10)
                ImGui.Text($"{Trial.TrialArea.Area.Name} (Act {Trial.TrialArea.Area.Act})");
            else
                ImGui.Text($"{Trial.TrialArea.Area.Name} (Maps)");
            ImGui.PopStyleColor(1);
        }
    }
}