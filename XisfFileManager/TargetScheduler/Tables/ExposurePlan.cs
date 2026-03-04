namespace XisfFileManager.TargetScheduler.Tables
{
    /*
    CREATE TABLE `exposureplan` (
	`Id`					INTEGER NOT NULL,
	`profileId`				TEXT NOT NULL,
	`exposure`				REAL NOT NULL,
	`desired`				INTEGER,
	`acquired`				INTEGER,
	`accepted`				INTEGER,
	`targetid`				INTEGER,
	`exposureTemplateId`	INTEGER,
	FOREIGN KEY(`targetId`) 
	REFERENCES `target`(`Id`),
	FOREIGN KEY(`exposureTemplateId`) 
	REFERENCES `exposuretemplate`(`Id`),
	PRIMARY KEY(`Id`)
	)
	*/
    internal sealed class ExposurePlan
    {
        public int Id { get; set; }
        public string profileId { get; set; } = string.Empty;
        public double exposure { get; set; }
        public int desired { get; set; }
        public int acquired { get; set; }
        public int accepted { get; set; }
        public int targetid { get; set; }
        public int exposureTemplateId { get; set; }
    }
}
