namespace XisfFileManager.TargetScheduler.Tables
{
    /*
     * CREATE TABLE `target` (
	`Id`					INTEGER NOT NULL,
	`name`					TEXT NOT NULL,
	`active`				INTEGER NOT NULL,
	`ra`					REAL,
	`dec`					REAL,
	`epochcode`				INTEGER NOT NULL,
	`rotation`				REAL,
	`roi`					REAL,
	`projectid`				INTEGER, 
	`overrideExposureOrder`	TEXT,
	FOREIGN KEY(`projectId`) 
	REFERENCES `project`(`Id`),
	PRIMARY KEY(`id`)
	)
	*/
    internal sealed class Target
    {
        public int Id { get; set; }
        public string name { get; set; } = string.Empty;
        public int active { get; set; }
        public double ra { get; set; }
        public double dec { get; set; }
        public int epochcode { get; set; }
        public double rotation { get; set; }
        public double roi { get; set; }
        public int projectid { get; set; }
        public string overrideExposureOrder { get; set; } = string.Empty;
    }
}
