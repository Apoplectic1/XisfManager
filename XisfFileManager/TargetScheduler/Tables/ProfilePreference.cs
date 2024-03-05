namespace XisfFileManager.TargetScheduler.Tables
{
    /*
    CREATE TABLE `profilepreference` (
	`Id`			            INTEGER NOT NULL,
	`profileId`		            TEXT NOT NULL,
	`enableGradeRMS`	        INTEGER,
	`enableGradeStars`	        INTEGER,
	`enableGradeHFR`	        INTEGER,
	`maxGradingSampleSize`		INTEGER,
	`rmsPixelThreshold`			REAL,
	`detectedStarsSigmaFactor`	REAL,
	`hfrSigmaFactor`			REAL, 
    `acceptimprovement`         INTEGER DEFAULT 1, 
    `exposurethrottle`          REAL DEFAULT 125, 
    `parkonwait`                INTEGER DEFAULT 0,
	PRIMARY KEY(`id`)
)
    )
	*/
    public class ProfilePreference
    {
        public int Id { get; set; }
        public string profileId { get; set; }
        public int enableGradeRMS { get; set; }
        public int enableGradeStars { get; set; }
        public int enableGradeHFR { get; set; }
        public int maxGradingSampleSize { get; set; }
        public double rmsPixelThreshold { get; set; }
        public double detectedStarsSigmaFactor { get; set; }
        public double hfrSigmaFactor { get; set; }
        public int acceptimprovement { get; set; } = 1;
        public double exposurethrottle { get; set; } = 125;
        public int parkonwait { get; set; }
    }
}
