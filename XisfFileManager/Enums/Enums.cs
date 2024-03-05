namespace XisfFileManager.Enums
{
    // These are the defineds for image frame types
    public enum eFrame { ALL, LIGHT, DARK, FLAT, BIAS, EMPTY }

    // Each image has to be associated with a single Filter
    public enum eFilter { ALL, LUMA, RED, GREEN, BLUE, HA, O3, S2, SHUTTER, EMPTY }

    // These specify how frame numbering will occur during renumbering of the files on disk
    public enum eOrder { WEIGHTINDEX, INDEXWEIGHT, WEIGHT, INDEX }

    public enum eCamera { ALL, Z183, Z533, Q178, A144 }

    // These control the display and clearing of the MessageBox for the results of finding and matching calibration files
    public enum eMessageMode { CLEAR, APPEND, NEW, KEEP }

    // In files streaming read and write operations, these define the type of data the buffer being read or written to will contain
    public enum eBufferData { ASCII, BINARY, ZEROS, USERDATA, POSITION }

    // Target Scheduler 
    public enum eProjectPriority { LOW = 0, NORMAL = 1, HIGH = 2 }

    // Main Form Keyword Upadate Mode
    public enum eKeywordUpdateMode { PROTECT, UPDATE_NEW, FORCE }
}
