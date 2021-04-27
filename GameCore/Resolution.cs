namespace GameCore
{
    public struct Resolution
    {
        private readonly int Multiplier;
        public int CurrentMultiplier => Multiplier;
        private readonly int ScreenWidth;
        public int Width => ScreenWidth;
        private readonly int ScreenHeight;
        public int Height => ScreenHeight;
        private readonly int FrameRate;
        public int FPS => FrameRate;

        public int NumberOfPixels => ScreenHeight * ScreenWidth;
        public int NumberOfPixelsPerSeconds => ScreenHeight * ScreenWidth * FrameRate;

        public Resolution(int screenWidth, int screenHeight, int frameRate)
        {
            Multiplier = 0;
            ScreenWidth = screenWidth;
            ScreenHeight = screenHeight;
            FrameRate = frameRate;
        }
        
        public Resolution(int screenWidth, int screenHeight)
        {
            Multiplier = 0;
            ScreenWidth = screenWidth;
            ScreenHeight = screenHeight;
            FrameRate = 60;
        }
        
        public Resolution(int screenWidth, int screenHeight, int frameRate, int multiplier)
        {
            Multiplier = multiplier;
            ScreenWidth = screenWidth;
            ScreenHeight = screenHeight;
            FrameRate = frameRate;
        }

        private int DeduceMultiplier()
        {
            //Todo
            return 1;
        }

        public override string ToString()
        {
            return ScreenWidth + "*" + ScreenHeight + "@" + FrameRate + "[x" + Multiplier + "]";
        }

        public double GetAspectRatio()
        {
            double aspectRatio = 0.0;
            if (ScreenWidth != 0)
                aspectRatio = ScreenHeight / ScreenWidth;
            else
                aspectRatio = ScreenHeight;
            return aspectRatio;
        }

        public string GetAspectRatioString()
        {
            Fraction aspectRatio = new Fraction(ScreenWidth, ScreenHeight);
            aspectRatio.Simplify();
            return aspectRatio.Numerator + ":" + aspectRatio.Denominator;
        }
    }
}