using Microsoft.Data.Sqlite;
using System.Linq;
using XisfFileManager.TargetScheduler.Tables;

namespace XisfFileManager.TargetScheduler
{
    internal class SqlLiteReader
    {
        private SqlLiteManager mSqlManager;

        public SqlLiteReader(SqlLiteManager manager)
        {
            mSqlManager = manager;
        }

        private void ClearAllTables()
        {
            mSqlManager.mAcquiredImageList.Clear();
            mSqlManager.mExposurePlanList.Clear();
            mSqlManager.mExposureTemplateList.Clear();
            mSqlManager.mImageDataList.Clear();
            mSqlManager.mProfilePreferenceList.Clear();
            mSqlManager.mProjectList.Clear();
            mSqlManager.mRuleWeightList.Clear();
            mSqlManager.mTargetList.Clear();
        }

        public bool ReadTargetSchedulerDataBaseFile(string sqlLightFileName)
        {
            ClearAllTables();

            using (SqliteConnection connection = new SqliteConnection($"Data Source={sqlLightFileName};"))
            {
                connection.Open();

                // Read the contents of all database tables

                // profilepreference Table
                using (SqliteCommand command = new SqliteCommand("SELECT * FROM profilepreference", connection))
                {
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ProfilePreference profilepreferenceRow = new ProfilePreference
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                profileId = reader.GetString(reader.GetOrdinal("profileId")),
                                enableGradeRMS = reader.GetInt32(reader.GetOrdinal("enableGradeRMS")),
                                enableGradeStars = reader.GetInt32(reader.GetOrdinal("enableGradeStars")),
                                enableGradeHFR = reader.GetInt32(reader.GetOrdinal("enableGradeHFR")),
                                maxGradingSampleSize = reader.GetInt32(reader.GetOrdinal("maxGradingSampleSize")),
                                rmsPixelThreshold = reader.GetDouble(reader.GetOrdinal("rmsPixelThreshold")),
                                detectedStarsSigmaFactor = reader.GetDouble(reader.GetOrdinal("detectedStarsSigmaFactor")),
                                hfrSigmaFactor = reader.GetDouble(reader.GetOrdinal("hfrSigmaFactor")),
                                acceptimprovement = reader.GetInt32(reader.GetOrdinal("acceptimprovement")),
                                exposurethrottle = reader.GetDouble(reader.GetOrdinal("exposurethrottle")),
                                parkonwait = reader.GetInt32(reader.GetOrdinal("parkonwait")),
                            };

                            mSqlManager.mProfilePreferenceList.Add(profilepreferenceRow);
                        }
                    }
                }

                // project Table
                using (SqliteCommand command = new SqliteCommand("SELECT * FROM project", connection))
                {
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Project projectRow = new Project
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                profileId = reader.GetString(reader.GetOrdinal("profileId")),
                                name = reader.GetString(reader.GetOrdinal("name")),
                                description = reader.GetString(reader.GetOrdinal("description")),
                                state = reader.GetInt32(reader.GetOrdinal("state")),
                                priority = reader.GetInt32(reader.GetOrdinal("priority")),
                                createdate = reader.GetInt32(reader.GetOrdinal("createdate")),
                                //activedate = reader.GetInt32(reader.GetOrdinal("activedate")),
                                //inactivedate = reader.GetInt32(reader.GetOrdinal("inactivedate")),
                                minimumtime = reader.GetInt32(reader.GetOrdinal("minimumtime")),
                                minimumaltitude = reader.GetDouble(reader.GetOrdinal("minimumaltitude")),
                                usecustomhorizon = reader.GetInt32(reader.GetOrdinal("usecustomhorizon")),
                                horizonoffset = reader.GetDouble(reader.GetOrdinal("horizonoffset")),
                                meridianwindow = reader.GetInt32(reader.GetOrdinal("meridianwindow")),
                                filterswitchfrequency = reader.GetInt32(reader.GetOrdinal("filterswitchfrequency")),
                                ditherevery = reader.GetInt32(reader.GetOrdinal("ditherevery")),
                                enablegrader = reader.GetInt32(reader.GetOrdinal("enablegrader")),
                                isMosaic = reader.GetInt32(reader.GetOrdinal("isMosaic")),
                            };

                            mSqlManager.mProjectList.Add(projectRow);
                        }
                    }

                    mSqlManager.mProjectList = mSqlManager.mProjectList.OrderBy(project => project.name).ToList();
                }

                // target Table
                using (SqliteCommand command = new SqliteCommand("SELECT * FROM target", connection))
                {
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Target targetRow = new Target
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                name = reader.GetString(reader.GetOrdinal("name")),
                                active = reader.GetInt32(reader.GetOrdinal("active")),
                                ra = reader.GetDouble(reader.GetOrdinal("ra")),
                                dec = reader.GetDouble(reader.GetOrdinal("dec")),
                                epochcode = reader.GetInt32(reader.GetOrdinal("epochcode")),
                                rotation = reader.GetDouble(reader.GetOrdinal("rotation")),
                                roi = reader.GetDouble(reader.GetOrdinal("roi")),
                                projectid = reader.GetInt32(reader.GetOrdinal("projectid")),
                                //overrideExposureOrder = reader.GetString(reader.GetOrdinal("overrideExposureOrder")),
                            };

                            mSqlManager.mTargetList.Add(targetRow);
                        }
                    }
                    mSqlManager.mTargetList = mSqlManager.mTargetList.OrderBy(target => target.name).ToList();
                }

                // exposureplan Table
                using (SqliteCommand command = new SqliteCommand("SELECT * FROM exposureplan", connection))
                {
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ExposurePlan exposurePlanRow = new ExposurePlan
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                profileId = reader.GetString(reader.GetOrdinal("profileId")),
                                exposure = reader.GetDouble(reader.GetOrdinal("exposure")),
                                desired = reader.GetInt32(reader.GetOrdinal("desired")),
                                acquired = reader.GetInt32(reader.GetOrdinal("acquired")),
                                accepted = reader.GetInt32(reader.GetOrdinal("accepted")),
                                targetid = reader.GetInt32(reader.GetOrdinal("targetid")),
                                exposureTemplateId = reader.GetInt32(reader.GetOrdinal("exposureTemplateId")),
                            };

                            mSqlManager.mExposurePlanList.Add(exposurePlanRow);
                        }
                    }
                }

                // exposuretemplate Table
                using (SqliteCommand command = new SqliteCommand("SELECT * FROM exposuretemplate", connection))
                {
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ExposureTemplate exposureTemplateRow = new ExposureTemplate
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                profileId = reader.GetString(reader.GetOrdinal("profileId")),
                                name = reader.GetString(reader.GetOrdinal("name")),
                                filtername = reader.GetString(reader.GetOrdinal("filtername")),
                                gain = reader.GetInt32(reader.GetOrdinal("gain")),
                                offset = reader.GetInt32(reader.GetOrdinal("offset")),
                                bin = reader.GetInt32(reader.GetOrdinal("bin")),
                                readoutmode = reader.GetInt32(reader.GetOrdinal("readoutmode")),
                                twilightlevel = reader.GetInt32(reader.GetOrdinal("twilightlevel")),
                                moonavoidanceenabled = reader.GetInt32(reader.GetOrdinal("moonavoidanceenabled")),
                                moonavoidanceseparation = reader.GetInt32(reader.GetOrdinal("moonavoidanceseparation")),
                                moonavoidancewidth = reader.GetInt32(reader.GetOrdinal("moonavoidancewidth")),
                                maximumhumidity = reader.GetInt32(reader.GetOrdinal("maximumhumidity")),
                                defaultexposure = reader.GetInt32(reader.GetOrdinal("defaultexposure")),
                            };

                            mSqlManager.mExposureTemplateList.Add(exposureTemplateRow);
                        }
                    }
                }

                // acquiredimage Table
                using (SqliteCommand command = new SqliteCommand("SELECT * FROM acquiredimage", connection))
                {
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            AcquiredImage acquiredImageRow = new AcquiredImage
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                projectId = reader.GetInt32(reader.GetOrdinal("projectId")),
                                targetId = reader.GetInt32(reader.GetOrdinal("targetId")),
                                acquireddate = reader.GetInt32(reader.GetOrdinal("acquireddate")),
                                filtername = reader.GetString(reader.GetOrdinal("filtername")),
                                accepted = reader.GetInt32(reader.GetOrdinal("accepted")),
                                metadata = reader.GetString(reader.GetOrdinal("metadata")),
                                rejectreason = reader.IsDBNull(reader.GetOrdinal("rejectreason")) ? string.Empty : reader.GetString(reader.GetOrdinal("rejectreason")),
                            };

                            mSqlManager.mAcquiredImageList.Add(acquiredImageRow);
                        }
                    }
                }

                // ruleweight Table
                using (SqliteCommand command = new SqliteCommand("SELECT * FROM ruleweight", connection))
                {
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            RuleWeight ruleWeightRow = new RuleWeight
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                name = reader.GetString(reader.GetOrdinal("name")),
                                weight = reader.GetDouble(reader.GetOrdinal("weight")),
                                projectid = reader.GetInt32(reader.GetOrdinal("projectid")),
                            };

                            mSqlManager.mRuleWeightList.Add(ruleWeightRow);
                        }
                    }
                }

                // imagedata Table
                using (SqliteCommand command = new SqliteCommand("SELECT * FROM imagedata", connection))
                {
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ImageData imageDataRow = new ImageData
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                tag = reader.GetString(reader.GetOrdinal("tag")),
                                imagedata = reader.GetFieldValue<byte[]>(reader.GetOrdinal("imagedata")),
                                acquiredimageid = reader.GetInt32(reader.GetOrdinal("acquiredimageid")),
                            };

                            mSqlManager.mImageDataList.Add(imageDataRow);
                        }
                    }
                }
            }

            return true;
        }
    }
}