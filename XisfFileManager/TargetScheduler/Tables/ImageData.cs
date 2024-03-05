namespace XisfFileManager.TargetScheduler.Tables
{
    /*
    CREATE TABLE `imagedata` (
	`Id`				INTEGER NOT NULL,
	`tag`				TEXT,
	`imagedata`			BLOB,
	`acquiredimageid`	INTEGER,
	FOREIGN KEY(`acquiredImageId`) 
	REFERENCES `acquiredimage`(`Id`),
	PRIMARY KEY(`Id`)
	)
	*/
    internal sealed class ImageData
    {
        public int Id { get; set; }
        public string tag { get; set; }
        public byte[] imagedata { get; set; }
        public int acquiredimageid { get; set; }
    }
}
