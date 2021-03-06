using System;
using Microsoft.VisualBasic;

namespace GameCore
{
    public enum KindOfCity
    {
        NotAKindOfCity,
        Downtown,
        Rural,
        Skyscrapper,
        Subburb,
        CountrySide
    }

public enum VarietyDistribution
{
    NotAVarietyDistributionSetting,
    None,
    VeryLow,
    Low,
    Medium,
    High,
    VeryHigh
}

public enum ResidentialPurchasing
{
    NotSet,
    FundingOnly,
    Minimal,
    VeryLow,
    Low,
    Normal,
    High
}

public enum PublicTransport
{
    NoPublicTransport,
    VeryDense,
    Dense,
    NotDense,
    Sparse,
    Simple,
    Rudementary
}

public enum Difficulty
{
    NotADifficulty,
    VeryEasy,
    Easy,
    SlightChallenge,
    Normal,
    Challenging,
    VeryHard,
    Impossible
}

public enum Theme
{
    NotATheme,
    Asian,
    European,
    American
}

    public class NewGame
    {
        private int MapSizeX;
        public int SecondaryDiagonalSize => MapSizeX;
        private int MapSizeY;
        public int MainDiagonalSize => MapSizeY;
        private int MapSizeZ;
        public int MainHeight => MapSizeZ;

        private KindOfCity KindOfCity;
        public KindOfCity TypeOfCity => KindOfCity;
        private VarietyDistribution VarietyDistribution;
        public VarietyDistribution Distribution => VarietyDistribution;
        private DateTime DateTime;
        public DateTime StartingDateTime => DateTime;
        private PublicTransport PublicTransport;
        public PublicTransport LevelOfPublicTransport => PublicTransport;

        private Difficulty Difficulty;
        public Difficulty GameDifficulty
        {
            get => Difficulty;
            set => Difficulty = value;
        }
        private Theme Theme;
        public Theme GetTheme => Theme;
        private Map Map;
        public Map CurrentMap => Map;
        
        public NewGame(int mapSizeX, int mapSizeY, int mapSizeZ, KindOfCity kindOfCity, 
            VarietyDistribution varietyDistribution, DateTime dateTime, PublicTransport publicTransport, 
            Difficulty difficulty, Theme theme)
        { 
            MapSizeX = mapSizeX;
            MapSizeY = mapSizeY;
            MapSizeZ = mapSizeZ;
            KindOfCity = kindOfCity;
            VarietyDistribution = varietyDistribution;
            DateTime = dateTime;
            PublicTransport = publicTransport;
            Difficulty = difficulty;
            Theme = theme;
            Map = new Map(mapSizeX, mapSizeY, mapSizeZ, this);
        }

        public void SetDifficulty(Difficulty difficulty)
        {
            GameDifficulty = difficulty;
        }

        public void SetDifficulty(string difficultyString)
        {
            string shortenedDifficultyString = GeneralPurposeTools.SqueezeString(difficultyString).ToLower();
            Difficulty difficulty = Difficulty.NotADifficulty;
            switch (shortenedDifficultyString)
            {
                case "very easy":
                case "veryeasy":
                case "1":
                    difficulty = Difficulty.VeryEasy;
                    break;
                case "easy":
                case "2":
                    difficulty = Difficulty.Easy;
                    break;
                case "normal":
                case "standard":
                case "3":
                    difficulty = Difficulty.Normal;
                    break;
                case "slightly difficult":
                case "slightlydifficult":
                case "a bit difficult":
                case "abitdifficult":
                case "slightly challenging":
                case "slightlychallenging":
                case "a bit challenging":
                case "abitchallenging":
                case "a bit of a challenge":
                case "abitofachallenge":
                case "4":
                    difficulty = Difficulty.SlightChallenge;
                    break;
                case "difficult":
                case "hard":
                case "challenge":
                case "a challenge":
                case "challenging":
                case "5":
                    difficulty = Difficulty.Challenging;
                    break;
                case "very difficult":
                case "vey hard":
                case "a big challenge":
                case "abigchallenge":
                case "vey challenging":
                case "6":
                    difficulty = Difficulty.VeryHard;
                    break;
                case "impossible":
                case "deity":
                case "extremely hard":
                case "extremely challenging":
                case "hardest difficulty":
                case "7":
                    difficulty = Difficulty.Impossible;
                    break;
                default:
                    Console.Error.WriteLine("Warning: unknown difficulty string. Normal difficulty will be set");
                    difficulty = Difficulty.Normal;
                    break;
            }

            GameDifficulty = difficulty;
        }

    public void SetDifficulty(int value)
    {
        GameCore.Difficulty difficulty = Difficulty.NotADifficulty;
        switch (value)
        {
            case 0:
                difficulty = Difficulty.NotADifficulty;
                break;
            case 1:
                difficulty = Difficulty.VeryEasy;
                break;
            case 2:
                difficulty = Difficulty.Easy;
                break;
            case 3:
                difficulty = Difficulty.Normal;
                break;
            case 4:
                difficulty = Difficulty.SlightChallenge;
                break;
            case 5:
                difficulty = Difficulty.Challenging;
                break;
            case 6:
                difficulty = Difficulty.VeryHard;
                break;
            case 7:
                difficulty = Difficulty.Impossible;
                break;
        }

        GameDifficulty = difficulty;
    }

    public void SetDifficulty(NewGame newGame)
    {
        GameDifficulty = newGame.GameDifficulty;
    }
}

    public enum Currency
    {
        NotACurrency,
        Dollar,
        Euro
    }

    public enum MainSide
    {
        NotASide,
        Right,
        Left
    }

    public enum UnitOfMeasurement
    {
        NotAUnitOfMeasurement,
        GameTick,
        Second,
        Minute,
        Hour,
        Day,
        Month,
        Year
    }
    public struct BaseSettings
    {
        private MainSide MainSide;
        public MainSide MainDrivingSide => MainSide;
        private long TicksBetweenAutosave;
        public long AutoSaveInterval
        {
            get => TicksBetweenAutosave;
            set
            {
                if (value >= 0)
                    TicksBetweenAutosave = value;
                else
                    Console.Error.WriteLine($"Error: autosave interval has to be set to a positive value. {value} is negative." +
                                            "\nThe autosave will not change.");
            }
        }

        private Currency Currency; 
        public Currency CurrencyUnit => Currency;
        private Resolution Resolution;
        private int FontSize;
        private GraphicsPack GraphicsPack;
        private SoundPack SoundPack;
        private MusicPack MusicPack;
        public void SetAutoSaveInterval(int value)
        {
            AutoSaveInterval = value;
        }

        public void SetAutoSaveInterval(UnitOfMeasurement unitOfMeasurement, int value)
        {
            AutoSaveInterval = GeneralPurposeTools.TimeToGameTicks(unitOfMeasurement, value);
        }

        public void SetAutoSaveInterval(UnitOfMeasurement[] unitsOfMeasurement, int[] value)
        {
            long total = 0;
            int numberOfUnits = unitsOfMeasurement.Length;
            int numberOfValues = value.Length;
            if (numberOfUnits == numberOfValues)
                for (int i = 0; i < numberOfUnits; i++)
                    total += GeneralPurposeTools.TimeToGameTicks(unitsOfMeasurement[i], value[i]);
            else
                throw new ArgumentException($"Error: different amount of units of measurement declared ({numberOfUnits})" +
                                            $"compared to number of corresponding values ({numberOfValues} declared)");
            AutoSaveInterval = total;
        }
        public void SetAutoSaveInterval(DateTime dateTime)
        {
            int result = 1;
            result *= dateTime.Second * 60;
            result *= dateTime.Minute * 3600;
            result *= dateTime.Hour * 216000;
            result *= dateTime.Day * 5184000;
            result *= dateTime.Month * 158112000;
            result *= dateTime.Year * 1893456000;
            AutoSaveInterval = result;
        }

        public void SetAutoSaveInterval(string readDateInterval)
        {
            throw new NotImplementedException();
        }
        
        
    }
}