// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SiteDirectoryDao.cs" company="RHEA System S.A.">
//    Copyright (c) 2015-2019 RHEA System S.A.
//
//    Author: Sam Gerené, Merlin Bieze, Alex Vorobiev, Naron Phou, Alexander van Delft.
//
//    This file is part of CDP4 Web Services Community Edition. 
//    The CDP4 Web Services Community Edition is the RHEA implementation of ECSS-E-TM-10-25 Annex A and Annex C.
//    This is an auto-generated class. Any manual changes to this file will be overwritten!
//
//    The CDP4 Web Services Community Edition is free software; you can redistribute it and/or
//    modify it under the terms of the GNU Affero General Public
//    License as published by the Free Software Foundation; either
//    version 3 of the License, or (at your option) any later version.
//
//    The CDP4 Web Services Community Edition is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
//    Lesser General Public License for more details.
//
//    You should have received a copy of the GNU Affero General Public License
//    along with this program.  If not, see <http://www.gnu.org/licenses/>.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace CDP4Orm.Dao
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using CDP4Common.DTO;

    using Npgsql;
    using NpgsqlTypes;

    /// <summary>
    /// The SiteDirectory Data Access Object which acts as an ORM layer to the SQL database.
    /// </summary>
    public partial class SiteDirectoryDao : TopContainerDao, ISiteDirectoryDao
    {
        /// <summary>
        /// Read the data from the database.
        /// </summary>
        /// <param name="transaction">
        /// The current <see cref="NpgsqlTransaction"/> to the database.
        /// </param>
        /// <param name="partition">
        /// The database partition (schema) where the requested resource is stored.
        /// </param>
        /// <param name="ids">
        /// Ids to retrieve from the database.
        /// </param>
        /// <param name="isCachedDtoReadEnabledAndInstant">
        /// The value indicating whether to get cached last state of Dto from revision history.
        /// </param>
        /// <returns>
        /// List of instances of <see cref="CDP4Common.DTO.SiteDirectory"/>.
        /// </returns>
        public virtual IEnumerable<CDP4Common.DTO.SiteDirectory> Read(NpgsqlTransaction transaction, string partition, IEnumerable<Guid> ids = null, bool isCachedDtoReadEnabledAndInstant = false)
        {
            using (var command = new NpgsqlCommand())
            {
                var sqlBuilder = new System.Text.StringBuilder();

                if (isCachedDtoReadEnabledAndInstant)
                {
                    sqlBuilder.AppendFormat("SELECT \"Jsonb\" FROM \"{0}\".\"SiteDirectory_Cache\"", partition);

                    if (ids != null && ids.Any())
                    {
                        sqlBuilder.Append(" WHERE \"Iid\" = ANY(:ids)");
                        command.Parameters.Add("ids", NpgsqlDbType.Array | NpgsqlDbType.Uuid).Value = ids;
                    }

                    sqlBuilder.Append(";");

                    command.Connection = transaction.Connection;
                    command.Transaction = transaction;
                    command.CommandText = sqlBuilder.ToString();

                    // log the sql command 
                    this.LogCommand(command);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var thing = this.MapJsonbToDto(reader);
                            if (thing != null)
                            {
                                yield return thing as SiteDirectory;
                            }
                        }
                    }
                }
                else
                {
                    sqlBuilder.AppendFormat("SELECT * FROM \"{0}\".\"SiteDirectory_View\"", partition);

                    if (ids != null && ids.Any()) 
                    {
                        sqlBuilder.Append(" WHERE \"Iid\" = ANY(:ids)");
                        command.Parameters.Add("ids", NpgsqlDbType.Array | NpgsqlDbType.Uuid).Value = ids;
                    }

                    sqlBuilder.Append(";");

                    command.Connection = transaction.Connection;
                    command.Transaction = transaction;
                    command.CommandText = sqlBuilder.ToString();

                    // log the sql command 
                    this.LogCommand(command);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            yield return this.MapToDto(reader);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// The mapping from a database record to data transfer object.
        /// </summary>
        /// <param name="reader">
        /// An instance of the SQL reader.
        /// </param>
        /// <returns>
        /// A deserialized instance of <see cref="CDP4Common.DTO.SiteDirectory"/>.
        /// </returns>
        public virtual CDP4Common.DTO.SiteDirectory MapToDto(NpgsqlDataReader reader)
        {
            string tempCreatedOn;
            string tempLastModifiedOn;
            string tempModifiedOn;
            string tempName;
            string tempShortName;

            var valueDict = (Dictionary<string, string>)reader["ValueTypeSet"];
            var iid = Guid.Parse(reader["Iid"].ToString());
            var revisionNumber = int.Parse(valueDict["RevisionNumber"]);

            var dto = new CDP4Common.DTO.SiteDirectory(iid, revisionNumber);
            dto.Annotation.AddRange(Array.ConvertAll((string[])reader["Annotation"], Guid.Parse));
            dto.DefaultParticipantRole = reader["DefaultParticipantRole"] is DBNull ? (Guid?)null : Guid.Parse(reader["DefaultParticipantRole"].ToString());
            dto.DefaultPersonRole = reader["DefaultPersonRole"] is DBNull ? (Guid?)null : Guid.Parse(reader["DefaultPersonRole"].ToString());
            dto.Domain.AddRange(Array.ConvertAll((string[])reader["Domain"], Guid.Parse));
            dto.DomainGroup.AddRange(Array.ConvertAll((string[])reader["DomainGroup"], Guid.Parse));
            dto.ExcludedDomain.AddRange(Array.ConvertAll((string[])reader["ExcludedDomain"], Guid.Parse));
            dto.ExcludedPerson.AddRange(Array.ConvertAll((string[])reader["ExcludedPerson"], Guid.Parse));
            dto.LogEntry.AddRange(Array.ConvertAll((string[])reader["LogEntry"], Guid.Parse));
            dto.Model.AddRange(Array.ConvertAll((string[])reader["Model"], Guid.Parse));
            dto.NaturalLanguage.AddRange(Array.ConvertAll((string[])reader["NaturalLanguage"], Guid.Parse));
            dto.Organization.AddRange(Array.ConvertAll((string[])reader["Organization"], Guid.Parse));
            dto.ParticipantRole.AddRange(Array.ConvertAll((string[])reader["ParticipantRole"], Guid.Parse));
            dto.Person.AddRange(Array.ConvertAll((string[])reader["Person"], Guid.Parse));
            dto.PersonRole.AddRange(Array.ConvertAll((string[])reader["PersonRole"], Guid.Parse));
            dto.SiteReferenceDataLibrary.AddRange(Array.ConvertAll((string[])reader["SiteReferenceDataLibrary"], Guid.Parse));

            if (valueDict.TryGetValue("CreatedOn", out tempCreatedOn))
            {
                dto.CreatedOn = Utils.ParseUtcDate(tempCreatedOn);
            }

            if (valueDict.TryGetValue("LastModifiedOn", out tempLastModifiedOn))
            {
                dto.LastModifiedOn = Utils.ParseUtcDate(tempLastModifiedOn);
            }

            if (valueDict.TryGetValue("ModifiedOn", out tempModifiedOn))
            {
                dto.ModifiedOn = Utils.ParseUtcDate(tempModifiedOn);
            }

            if (valueDict.TryGetValue("Name", out tempName))
            {
                dto.Name = tempName.UnEscape();
            }

            if (valueDict.TryGetValue("ShortName", out tempShortName))
            {
                dto.ShortName = tempShortName.UnEscape();
            }

            return dto;
        }

        /// <summary>
        /// Insert a new database record from the supplied data transfer object.
        /// </summary>
        /// <param name="transaction">
        /// The current <see cref="NpgsqlTransaction"/> to the database.
        /// </param>
        /// <param name="partition">
        /// The database partition (schema) where the requested resource will be stored.
        /// </param>
        /// <param name="siteDirectory">
        /// The siteDirectory DTO that is to be persisted.
        /// </param>
        /// <param name="container">
        /// The container of the DTO to be persisted.
        /// </param>
        /// <returns>
        /// True if the concept was successfully persisted.
        /// </returns>
        public virtual bool Write(NpgsqlTransaction transaction, string partition, CDP4Common.DTO.SiteDirectory siteDirectory, CDP4Common.DTO.Thing container = null)
        {
            bool isHandled;
            var valueTypeDictionaryAdditions = new Dictionary<string, string>();
            var beforeWrite = this.BeforeWrite(transaction, partition, siteDirectory, container, out isHandled, valueTypeDictionaryAdditions);
            if (!isHandled)
            {
                beforeWrite = beforeWrite && base.Write(transaction, partition, siteDirectory, container);

                var valueTypeDictionaryContents = new Dictionary<string, string>
                {
                    { "CreatedOn", !this.IsDerived(siteDirectory, "CreatedOn") ? siteDirectory.CreatedOn.ToString(Utils.DateTimeUtcSerializationFormat) : string.Empty },
                    { "Name", !this.IsDerived(siteDirectory, "Name") ? siteDirectory.Name.Escape() : string.Empty },
                    { "ShortName", !this.IsDerived(siteDirectory, "ShortName") ? siteDirectory.ShortName.Escape() : string.Empty },
                }.Concat(valueTypeDictionaryAdditions).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

                using (var command = new NpgsqlCommand())
                {
                    var sqlBuilder = new System.Text.StringBuilder();
                    
                    sqlBuilder.AppendFormat("INSERT INTO \"{0}\".\"SiteDirectory\"", partition);
                    sqlBuilder.AppendFormat(" (\"Iid\", \"ValueTypeDictionary\", \"DefaultParticipantRole\", \"DefaultPersonRole\")");
                    sqlBuilder.AppendFormat(" VALUES (:iid, :valueTypeDictionary, :defaultParticipantRole, :defaultPersonRole);");

                    command.Parameters.Add("iid", NpgsqlDbType.Uuid).Value = siteDirectory.Iid;
                    command.Parameters.Add("valueTypeDictionary", NpgsqlDbType.Hstore).Value = valueTypeDictionaryContents;
                    command.Parameters.Add("defaultParticipantRole", NpgsqlDbType.Uuid).Value = !this.IsDerived(siteDirectory, "DefaultParticipantRole") ? Utils.NullableValue(siteDirectory.DefaultParticipantRole) : Utils.NullableValue(null);
                    command.Parameters.Add("defaultPersonRole", NpgsqlDbType.Uuid).Value = !this.IsDerived(siteDirectory, "DefaultPersonRole") ? Utils.NullableValue(siteDirectory.DefaultPersonRole) : Utils.NullableValue(null);

                    command.CommandText = sqlBuilder.ToString();
                    command.Connection = transaction.Connection;
                    command.Transaction = transaction;

                    this.ExecuteAndLogCommand(command);
                }
            }

            return this.AfterWrite(beforeWrite, transaction, partition, siteDirectory, container);
        }

        /// <summary>
        /// Update a database record from the supplied data transfer object.
        /// </summary>
        /// <param name="transaction">
        /// The current <see cref="NpgsqlTransaction"/> to the database.
        /// </param>
        /// <param name="partition">
        /// The database partition (schema) where the requested resource will be updated.
        /// </param>
        /// <param name="siteDirectory">
        /// The SiteDirectory DTO that is to be updated.
        /// </param>
        /// <param name="container">
        /// The container of the DTO to be updated.
        /// </param>
        /// <returns>
        /// True if the concept was successfully updated.
        /// </returns>
        public virtual bool Update(NpgsqlTransaction transaction, string partition, CDP4Common.DTO.SiteDirectory siteDirectory, CDP4Common.DTO.Thing container = null)
        {
            bool isHandled;
            var valueTypeDictionaryAdditions = new Dictionary<string, string>();
            var beforeUpdate = this.BeforeUpdate(transaction, partition, siteDirectory, container, out isHandled, valueTypeDictionaryAdditions);
            if (!isHandled)
            {
                beforeUpdate = beforeUpdate && base.Update(transaction, partition, siteDirectory, container);

                var valueTypeDictionaryContents = new Dictionary<string, string>
                {
                    { "CreatedOn", !this.IsDerived(siteDirectory, "CreatedOn") ? siteDirectory.CreatedOn.ToString(Utils.DateTimeUtcSerializationFormat) : string.Empty },
                    { "Name", !this.IsDerived(siteDirectory, "Name") ? siteDirectory.Name.Escape() : string.Empty },
                    { "ShortName", !this.IsDerived(siteDirectory, "ShortName") ? siteDirectory.ShortName.Escape() : string.Empty },
                }.Concat(valueTypeDictionaryAdditions).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

                using (var command = new NpgsqlCommand())
                {
                    var sqlBuilder = new System.Text.StringBuilder();
                    sqlBuilder.AppendFormat("UPDATE \"{0}\".\"SiteDirectory\"", partition);
                    sqlBuilder.AppendFormat(" SET (\"ValueTypeDictionary\", \"DefaultParticipantRole\", \"DefaultPersonRole\")");
                    sqlBuilder.AppendFormat(" = (:valueTypeDictionary, :defaultParticipantRole, :defaultPersonRole)");
                    sqlBuilder.AppendFormat(" WHERE \"Iid\" = :iid;");

                    command.Parameters.Add("iid", NpgsqlDbType.Uuid).Value = siteDirectory.Iid;
                    command.Parameters.Add("valueTypeDictionary", NpgsqlDbType.Hstore).Value = valueTypeDictionaryContents;
                    command.Parameters.Add("defaultParticipantRole", NpgsqlDbType.Uuid).Value = !this.IsDerived(siteDirectory, "DefaultParticipantRole") ? Utils.NullableValue(siteDirectory.DefaultParticipantRole) : Utils.NullableValue(null);
                    command.Parameters.Add("defaultPersonRole", NpgsqlDbType.Uuid).Value = !this.IsDerived(siteDirectory, "DefaultPersonRole") ? Utils.NullableValue(siteDirectory.DefaultPersonRole) : Utils.NullableValue(null);

                    command.CommandText = sqlBuilder.ToString();
                    command.Connection = transaction.Connection;
                    command.Transaction = transaction;

                    this.ExecuteAndLogCommand(command);
                }
            }

            return this.AfterUpdate(beforeUpdate, transaction, partition, siteDirectory, container);
        }

        /// <summary>
        /// Delete a database record from the supplied data transfer object.
        /// </summary>
        /// <param name="transaction">
        /// The current <see cref="NpgsqlTransaction"/> to the database.
        /// </param>
        /// <param name="partition">
        /// The database partition (schema) where the requested resource will be deleted.
        /// </param>
        /// <param name="iid">
        /// The <see cref="CDP4Common.DTO.SiteDirectory"/> id that is to be deleted.
        /// </param>
        /// <returns>
        /// True if the concept was successfully deleted.
        /// </returns>
        public override bool Delete(NpgsqlTransaction transaction, string partition, Guid iid)
        {
            bool isHandled;
            var beforeDelete = this.BeforeDelete(transaction, partition, iid, out isHandled);
            if (!isHandled)
            {
                beforeDelete = beforeDelete && base.Delete(transaction, partition, iid);
            }

            return this.AfterDelete(beforeDelete, transaction, partition, iid);
        }
    }
}
