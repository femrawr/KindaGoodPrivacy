using IniParser;

namespace KindaGoodPrivacy.Source.Utility
{
    public class Warning
    {
        public static bool VideoPlayerWarning()
        {
            var parser = new FileIniDataParser();
            var data = parser.ReadFile(Path.Join(Core.Store.path, "State.ini"));

            if (!data.Sections.ContainsSection("state"))
            {
                data.Sections.AddSection("state");
            }

            if (data["state"]["pressed_ok_on_video_player_warning"] == "true")
            {
                return true;
            }

            var res = MessageBox.Show(
                "When playing videos, the video has to be decrypted and written to a file for the media player to display it. This might introduce potential vulnerabilities. Press \"Cancel\" if you do not trust this process.",
                "Video player",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Exclamation
            );

            if (res != DialogResult.OK)
            {
                return false;
            }

            data["state"]["pressed_ok_on_video_player_warning"] = "true";
            parser.WriteFile(Path.Join(Core.Store.path, "State.ini"), data);

            return true;
        }
    }
}
