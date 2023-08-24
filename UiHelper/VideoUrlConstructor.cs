namespace ShikimoriMe.UiHelper
{
    static class VideoUrlConstructor
    {
        public static string Construct(string url)
        {
            //TODO: Поменять на нормальный парсинг video_id!
            return $@"
                <html>
                    <body>
                        <iframe width='100%' height='100%'
                            src='https://www.youtube.com/embed/{url.Substring(17)}?autoplay=0'
                            frameborder='0' allowfullscreen>
                        </iframe>
                    </body>
                </html>";
        }
    }
}