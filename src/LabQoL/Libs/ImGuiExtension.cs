using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ImGuiNET;
using PoeHUD.Framework;
using PoeHUD.Hud.Settings;
using PoeHUD.Plugins;
using SharpDX;
using ImGuiVector2 = System.Numerics.Vector2;
using ImGuiVector4 = System.Numerics.Vector4;

namespace Random_Features.Libs
{
    public class ImGuiExtension
    {
        public static ImGuiVector4 CenterWindow(int Width, int Height)
        {
            var CenterPos = BasePlugin.API.GameController.Window.GetWindowRectangle().Center;
            return new ImGuiVector4(Width + CenterPos.X - Width / 2, Height + CenterPos.Y - Height / 2, Width, Height);
        }

        public static bool BeginWindow(string Title, ref bool IsOpened, int X, int Y, int Width, int Height, bool AutoResize = false)
        {
            ImGui.SetNextWindowPos(new ImGuiVector2(Width + X, Height + Y), Condition.Appearing, new ImGuiVector2(1, 1));
            ImGui.SetNextWindowSize(new ImGuiVector2(Width, Height), Condition.Appearing);
            return ImGui.BeginWindow(Title, ref IsOpened, AutoResize ? WindowFlags.AlwaysAutoResize : WindowFlags.Default);
        }

        public static bool BeginWindow(string Title, ref bool IsOpened, float X, float Y, float Width, float Height, bool AutoResize = false)
        {
            ImGui.SetNextWindowPos(new ImGuiVector2(Width + X, Height + Y), Condition.Appearing, new ImGuiVector2(1, 1));
            ImGui.SetNextWindowSize(new ImGuiVector2(Width, Height), Condition.Appearing);
            return ImGui.BeginWindow(Title, ref IsOpened, AutoResize ? WindowFlags.AlwaysAutoResize : WindowFlags.Default);
        }

        public static bool BeginWindowCenter(string Title, ref bool IsOpened, int Width, int Height, bool AutoResize = false)
        {
            var Size = CenterWindow(Width, Height);
            ImGui.SetNextWindowPos(new ImGuiVector2(Size.X, Size.Y), Condition.Appearing, new ImGuiVector2(1, 1));
            ImGui.SetNextWindowSize(new ImGuiVector2(Size.Z, Size.W), Condition.Appearing);
            return ImGui.BeginWindow(Title, ref IsOpened, AutoResize ? WindowFlags.AlwaysAutoResize : WindowFlags.Default);
        }

        // Int Sliders
        public static int IntSlider(string LabelString, int Value, int MinValue, int MaxValue)
        {
            var RefValue = Value;
            ImGui.SliderInt(LabelString, ref RefValue, MinValue, MaxValue, "%.00f");
            return RefValue;
        }

        public static int IntSlider(string LabelString, string SliderString, int Value, int MinValue, int MaxValue)
        {
            var RefValue = Value;
            ImGui.SliderInt(LabelString, ref RefValue, MinValue, MaxValue, $"{SliderString}: {Value}");
            return RefValue;
        }

        public static int IntSlider(string LabelString, RangeNode<int> Setting)
        {
            var RefValue = Setting.Value;
            ImGui.SliderInt(LabelString, ref RefValue, Setting.Min, Setting.Max, "%.00f");
            return RefValue;
        }

        public static int IntSlider(string LabelString, string SliderString, RangeNode<int> Setting)
        {
            var RefValue = Setting.Value;
            ImGui.SliderInt(LabelString, ref RefValue, Setting.Min, Setting.Max, $"{SliderString}: {Setting.Value}");
            return RefValue;
        }

        // float Sliders
        public static float FloatSlider(string LabelString, float Value, float MinValue, float MaxValue)
        {
            var RefValue = Value;
            ImGui.SliderFloat(LabelString, ref RefValue, MinValue, MaxValue, "%.00f", 1f);
            return RefValue;
        }

        public static float FloatSlider(string LabelString, float Value, float MinValue, float MaxValue, float Power)
        {
            var RefValue = Value;
            ImGui.SliderFloat(LabelString, ref RefValue, MinValue, MaxValue, "%.00f", Power);
            return RefValue;
        }

        public static float FloatSlider(string LabelString, string SliderString, float Value, float MinValue, float MaxValue)
        {
            var RefValue = Value;
            ImGui.SliderFloat(LabelString, ref RefValue, MinValue, MaxValue, $"{SliderString}: {Value}", 1f);
            return RefValue;
        }

        public static float FloatSlider(string LabelString, string SliderString, float Value, float MinValue, float MaxValue, float Power)
        {
            var RefValue = Value;
            ImGui.SliderFloat(LabelString, ref RefValue, MinValue, MaxValue, $"{SliderString}: {Value}", Power);
            return RefValue;
        }

        public static float FloatSlider(string LabelString, RangeNode<float> Setting)
        {
            var RefValue = Setting.Value;
            ImGui.SliderFloat(LabelString, ref RefValue, Setting.Min, Setting.Max, "%.00f", 1f);
            return RefValue;
        }

        public static float FloatSlider(string LabelString, RangeNode<float> Setting, float Power)
        {
            var RefValue = Setting.Value;
            ImGui.SliderFloat(LabelString, ref RefValue, Setting.Min, Setting.Max, "%.00f", Power);
            return RefValue;
        }

        public static float FloatSlider(string LabelString, string SliderString, RangeNode<float> Setting)
        {
            var RefValue = Setting.Value;
            ImGui.SliderFloat(LabelString, ref RefValue, Setting.Min, Setting.Max, $"{SliderString}: {Setting.Value}", 1f);
            return RefValue;
        }

        public static float FloatSlider(string LabelString, string SliderString, RangeNode<float> Setting, float Power)
        {
            var RefValue = Setting.Value;
            ImGui.SliderFloat(LabelString, ref RefValue, Setting.Min, Setting.Max, $"{SliderString}: {Setting.Value}", Power);
            return RefValue;
        }

        // Checkboxes
        public static bool Checkbox(string LabelString, bool BoolValue)
        {
            ImGui.Checkbox(LabelString, ref BoolValue);
            return BoolValue;
        }

        public static bool Checkbox(string LabelString, bool BoolValue, out bool OutBool)
        {
            ImGui.Checkbox(LabelString, ref BoolValue);
            OutBool = BoolValue;
            return BoolValue;
        }

        // Hotkey Selector
        public static IEnumerable<Keys> KeyCodes() => Enum.GetValues(typeof(Keys)).Cast<Keys>();

        public static Keys HotkeySelector(string ButtonName, Keys CurrentKey)
        {
            if (ImGui.Button($"{ButtonName}: {CurrentKey} ")) ImGui.OpenPopup(ButtonName);
            if (ImGui.BeginPopupModal(ButtonName, (WindowFlags) 35))
            {
                ImGui.Text($"Press a key to set as {ButtonName}");
                foreach (var Key in KeyCodes())
                {
                    if (!WinApi.IsKeyDown(Key)) continue;
                    if (Key != Keys.Escape && Key != Keys.RButton && Key != Keys.LButton)
                    {
                        ImGui.CloseCurrentPopup();
                        ImGui.EndPopup();
                        return Key;
                    }

                    break;
                }

                ImGui.EndPopup();
            }

            return CurrentKey;
        }

        public static Keys HotkeySelector(string ButtonName, string PopupTitle, Keys CurrentKey)
        {
            if (ImGui.Button($"{ButtonName}: {CurrentKey} ")) ImGui.OpenPopup(PopupTitle);
            if (ImGui.BeginPopupModal(PopupTitle, (WindowFlags) 35))
            {
                ImGui.Text($"Press a key to set as {ButtonName}");
                foreach (var Key in KeyCodes())
                {
                    if (!WinApi.IsKeyDown(Key)) continue;
                    if (Key != Keys.Escape && Key != Keys.RButton && Key != Keys.LButton)
                    {
                        ImGui.CloseCurrentPopup();
                        ImGui.EndPopup();
                        return Key;
                    }

                    break;
                }

                ImGui.EndPopup();
            }

            return CurrentKey;
        }

        // Color Pickers
        public static Color ColorPicker(string LabelName, Color InputColor)
        {
            var Color = InputColor.ToVector4();
            var ColorToVect4 = new ImGuiVector4(Color.X, Color.Y, Color.Z, Color.W);
            if (ImGui.ColorEdit4(LabelName, ref ColorToVect4, ColorEditFlags.AlphaBar)) return new Color(ColorToVect4.X, ColorToVect4.Y, ColorToVect4.Z, ColorToVect4.W);
            return InputColor;
        }

        // Combo Box

        public static int ComboBox(string SideLabel, int CurrentSelectedItem, List<string> ObjectList, ComboFlags ComboFlags = ComboFlags.HeightRegular)
        {
            ImGui.Combo(SideLabel, ref CurrentSelectedItem, ObjectList.ToArray());
            return CurrentSelectedItem;
        }

        public static string ComboBox(string SideLabel, string CurrentSelectedItem, List<string> ObjectList, ComboFlags ComboFlags = ComboFlags.HeightRegular)
        {
            if (ImGui.BeginCombo(SideLabel, CurrentSelectedItem, ComboFlags))
            {
                var RefObject = CurrentSelectedItem;
                for (var N = 0; N < ObjectList.Count; N++)
                {
                    var IsSelected = RefObject == ObjectList[N];
                    if (ImGui.Selectable(ObjectList[N], IsSelected))
                    {
                        ImGui.EndCombo();
                        return ObjectList[N];
                    }

                    if (IsSelected) ImGui.SetItemDefaultFocus();
                }

                ImGui.EndCombo();
            }

            return CurrentSelectedItem;
        }

        public static string ComboBox(string SideLabel, string CurrentSelectedItem, List<string> ObjectList, out bool DidChange, ComboFlags ComboFlags = ComboFlags.HeightRegular)
        {
            if (ImGui.BeginCombo(SideLabel, CurrentSelectedItem, ComboFlags))
            {
                var RefObject = CurrentSelectedItem;
                for (var N = 0; N < ObjectList.Count; N++)
                {
                    var IsSelected = RefObject == ObjectList[N];
                    if (ImGui.Selectable(ObjectList[N], IsSelected))
                    {
                        DidChange = true;
                        ImGui.EndCombo();
                        return ObjectList[N];
                    }

                    if (IsSelected) ImGui.SetItemDefaultFocus();
                }

                ImGui.EndCombo();
            }

            DidChange = false;
            return CurrentSelectedItem;
        }

        // Unput Text
        public static unsafe string InputText(string Label, string CurrentValue, uint MaxLength, InputTextFlags Flags)
        {
            var CurrentStringBytes = Encoding.Default.GetBytes(CurrentValue);
            var Buffer = new byte[MaxLength];
            Array.Copy(CurrentStringBytes, Buffer, Math.Min(CurrentStringBytes.Length, MaxLength));

            int Callback(TextEditCallbackData* Data)
            {
                var PCursorPos = (int*) Data->UserData;
                if (!Data->HasSelection()) *PCursorPos = Data->CursorPos;
                return 0;
            }

            ImGui.InputText(Label, Buffer, MaxLength, Flags, Callback);
            return Encoding.Default.GetString(Buffer).TrimEnd('\0');
        }

        // ImColor_HSV Maker
        public static ImGuiVector4 ImColor_HSV(float H, float S, float V)
        {
            ImGui.ColorConvertHSVToRGB(H, S, V, out var R, out var G, out var B);
            return new ImGuiVector4(R, G, B, 255);
        }

        public static ImGuiVector4 ImColor_HSV(float H, float S, float V, float A)
        {
            ImGui.ColorConvertHSVToRGB(H, S, V, out var R, out var G, out var B);
            return new ImGuiVector4(R, G, B, A);
        }

        // Color menu tabs
        public static void ImGuiExtension_ColorTabs(string IdString, int Height, IReadOnlyList<string> SettingList, ref int SelectedItem, ref int UniqueIdPop)
        {
            ImGuiNative.igGetContentRegionAvail(out var NewcontentRegionArea);
            var BoxRegion = new ImGuiVector2(NewcontentRegionArea.X, Height);
            if (ImGui.BeginChild(IdString, BoxRegion, true, WindowFlags.HorizontalScrollbar))
            {
                for (var I = 0; I < SettingList.Count; I++)
                {
                    ImGui.PushID(UniqueIdPop);
                    var Hue = 1f / SettingList.Count * I;
                    ImGui.PushStyleColor(ColorTarget.Button, ImColor_HSV(Hue, 0.6f, 0.6f, 0.8f));
                    ImGui.PushStyleColor(ColorTarget.ButtonHovered, ImColor_HSV(Hue, 0.7f, 0.7f, 0.9f));
                    ImGui.PushStyleColor(ColorTarget.ButtonActive, ImColor_HSV(Hue, 0.8f, 0.8f, 1.0f));
                    ImGui.PushStyleVar(StyleVar.FrameRounding, 3.0f);
                    ImGui.PushStyleVar(StyleVar.FramePadding, 2.0f);
                    if (I > 0) ImGui.SameLine();
                    if (ImGui.Button(SettingList[I])) SelectedItem = I;
                    UniqueIdPop++;
                    ImGui.PopStyleVar();
                    ImGui.PopStyleColor(3);
                    ImGui.PopID();
                }

            }
            ImGui.EndChild();
        }

        //Begin Child Frames - Full Width
        public static bool BeginChild(string Id, bool Border, WindowFlags Flags)
        {
            ImGuiNative.igGetContentRegionAvail(out var NewcontentRegionArea);
            return ImGui.BeginChild(Id, new ImGuiVector2(NewcontentRegionArea.X, NewcontentRegionArea.Y), Border, Flags);
        }

        //Spacing
        public static void Spacing(int Amount)
        {
            for (var I = 0; I < Amount; I++)
                ImGui.Spacing();
        }

        //End Columns
        public static void EndColumn()
        {
            ImGui.Columns(1, null, false);
        }
    }
}