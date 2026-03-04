namespace XisfFileManager.TargetScheduler.Tables
{
    /*
    CREATE TABLE `project` (
	`Id`					INTEGER NOT NULL,
	`profileId`				TEXT NOT NULL,
	`name`					TEXT NOT NULL,
	`description`			TEXT,
	`state`					INTEGER,
	`priority`				INTEGER,
	`createdate`			INTEGER,
	`activedate`			INTEGER,
	`inactivedate`			INTEGER,
	`minimumtime`			INTEGER,
	`minimumaltitude`		REAL,
	`usecustomhorizon`		INTEGER,
	`horizonoffset`			REAL,
	`meridianwindow`		INTEGER,
	`filterswitchfrequency`	INTEGER,
	`ditherevery`			INTEGER,
	`enablegrader`			INTEGER, 
	`isMosaic`              INTEGER NOT NULL DEFAULT 0,
	PRIMARY KEY(`id`)
	)
	*/
    internal sealed class Project
    {
        public int Id { get; set; }
        public string profileId { get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public int state { get; set; }
        public int priority { get; set; }
        public int createdate { get; set; }
        public int? activedate { get; set; }
        public int? inactivedate { get; set; }
        public int minimumtime { get; set; }
        public double minimumaltitude { get; set; }
        public int usecustomhorizon { get; set; }
        public double horizonoffset { get; set; }
        public int meridianwindow { get; set; }
        public int filterswitchfrequency { get; set; }
        public int ditherevery { get; set; }
        public int enablegrader { get; set; }
        public int isMosaic { get; set; }
    }
}
