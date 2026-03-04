namespace XisfFileManager.TargetScheduler.Tables
{
    /*
    CREATE TABLE `acquiredimage` (
	`Id`			INTEGER NOT NULL,
	`projectId`		INTEGER NOT NULL,
	`targetId`		INTEGER NOT NULL,
	`acquireddate`	INTEGER,
	`filtername`	TEXT NOT NULL,
	`accepted`		INTEGER NOT NULL,
    `metadata`		TEXT NOT NULL, 
	`rejectreason`	TEXT,
	PRIMARY KEY(`Id`)
	)
	*/
    internal sealed class AcquiredImage
    {
        public int Id { get; set; }
        public int projectId { get; set; }
        public int targetId { get; set; }
        public int acquireddate { get; set; }
        public string filtername { get; set; } = string.Empty;
        public int accepted { get; set; }
        public string metadata { get; set; } = string.Empty;
        public string rejectreason { get; set; } = string.Empty;
    }
}
