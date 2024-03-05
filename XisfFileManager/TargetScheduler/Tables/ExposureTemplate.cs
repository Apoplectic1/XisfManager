namespace XisfFileManager.TargetScheduler.Tables
{
    /*
    CREATE TABLE `exposuretemplate` (
	`Id`						INTEGER NOT NULL,
    `profileId`					TEXT NOT NULL,
    `name`						TEXT NOT NULL,
    `filtername`				TEXT NOT NULL,
	`gain`						INTEGER,
	`offset`					INTEGER,
	`bin`						INTEGER,
	`readoutmode`				INTEGER,
	`twilightlevel`				INTEGER,
	`moonavoidanceenabled`		INTEGER,
	`moonavoidanceseparation`	REAL,
	`moonavoidancewidth`		INTEGER,
	`maximumhumidity`			REAL, 
	`defaultexposure`			REAL DEFAULT 60,
	PRIMARY KEY(`Id`)
	)	
	*/
    internal sealed class ExposureTemplate
    {
        public int Id { get; set; }
        public string profileId { get; set; }
        public string name { get; set; }
        public string filtername { get; set; }
        public int gain { get; set; }
        public int offset { get; set; }
        public int bin { get; set; }
        public int readoutmode { get; set; }
        public int twilightlevel { get; set; }
        public int moonavoidanceenabled { get; set; }
        public double moonavoidanceseparation { get; set; }
        public int moonavoidancewidth { get; set; }
        public double maximumhumidity { get; set; }
        public double defaultexposure { get; set; }
    }
}
