// --- auto-generated code --- do not modify ---

/*
{{StartPackageInfo}}
<PackageInfo xmlns="http://www.skyline.be/ClassLibrary">
	<BasePackage>
		<Identity>
			<Name>Class Library</Name>
			<Version>1.1.2.5</Version>
		</Identity>
	</BasePackage>
	<CustomPackages />
</PackageInfo>
{{EndPackageInfo}}
*/

namespace Skyline.DataMiner.Library.Common
{
    namespace Attributes
    {
        /// <summary>
        /// This attribute indicates a DLL is required.
        /// </summary>
        [System.AttributeUsage(System.AttributeTargets.All, AllowMultiple = true)]
        public sealed class DllImportAttribute : System.Attribute
        {
            /// <summary>
            /// Initializes a new instance of the <see cref = "DllImportAttribute"/> class.
            /// </summary>
            /// <param name = "dllImport">The name of the DLL to be imported.</param>
            public DllImportAttribute(string dllImport)
            {
                DllImport = dllImport;
            }

            /// <summary>
            /// Gets the name of the DLL to be imported.
            /// </summary>
            public string DllImport
            {
                get;
                private set;
            }
        }
    }

    /// <summary>
    /// Represents a system-wide element ID.
    /// </summary>
    /// <remarks>This is a combination of a DataMiner Agent ID (the ID of the Agent on which the element was created) and an element ID.</remarks>
    [System.Serializable]
    public struct DmsElementId : System.IEquatable<Skyline.DataMiner.Library.Common.DmsElementId>, System.IComparable, System.IComparable<Skyline.DataMiner.Library.Common.DmsElementId>
    {
        /// <summary>
        /// The DataMiner Agent ID.
        /// </summary>
        private readonly int agentId;
        /// <summary>
        /// The element ID.
        /// </summary>
        private readonly int elementId;
        /// <summary>
        /// Initializes a new instance of the <see cref = "DmsElementId"/> structure using the specified string.
        /// </summary>
        /// <param name = "id">String representing the system-wide element ID.</param>
        /// <remarks>The provided string must be formatted as follows: "DataMiner Agent ID/element ID (e.g. 400/201)".</remarks>
        /// <exception cref = "ArgumentNullException"><paramref name = "id"/> is <see langword = "null"/> .</exception>
        /// <exception cref = "ArgumentException"><paramref name = "id"/> is the empty string ("") or white space.</exception>
        /// <exception cref = "ArgumentException">The ID does not match the mandatory format.</exception>
        /// <exception cref = "ArgumentException">The DataMiner Agent ID is not an integer.</exception>
        /// <exception cref = "ArgumentException">The element ID is not an integer.</exception>
        /// <exception cref = "ArgumentException">Invalid DataMiner Agent ID.</exception>
        /// <exception cref = "ArgumentException">Invalid element ID.</exception>
        public DmsElementId(string id)
        {
            if (id == null)
            {
                throw new System.ArgumentNullException("id");
            }

            if (System.String.IsNullOrWhiteSpace(id))
            {
                throw new System.ArgumentException("The provided ID must not be empty.", "id");
            }

            string[] idParts = id.Split('/');
            if (idParts.Length != 2)
            {
                string message = System.String.Format(System.Globalization.CultureInfo.InvariantCulture, "Invalid ID. Value: {0}. The string must be formatted as follows: \"agent ID/element ID\".", id);
                throw new System.ArgumentException(message, "id");
            }

            if (!System.Int32.TryParse(idParts[0], System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.InvariantCulture, out agentId))
            {
                string message = System.String.Format(System.Globalization.CultureInfo.InvariantCulture, "Invalid DataMiner agent ID. \"{0}\" is not an integer value", id);
                throw new System.ArgumentException(message, "id");
            }

            if (!System.Int32.TryParse(idParts[1], System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.InvariantCulture, out elementId))
            {
                string message = System.String.Format(System.Globalization.CultureInfo.InvariantCulture, "Invalid Element ID. \"{0}\" is not an integer value", id);
                throw new System.ArgumentException(message, "id");
            }

            if (!IsValidAgentId())
            {
                string message = System.String.Format(System.Globalization.CultureInfo.InvariantCulture, "Invalid agent ID. Value: {0}.", agentId);
                throw new System.ArgumentException(message, "id");
            }

            if (!IsValidElementId())
            {
                string message = System.String.Format(System.Globalization.CultureInfo.InvariantCulture, "Invalid element ID. Value: {0}.", elementId);
                throw new System.ArgumentException(message, "id");
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref = "DmsElementId"/> structure using the specified element ID and DataMiner Agent ID.
        /// </summary>
        /// <param name = "agentId">The DataMiner Agent ID.</param>
        /// <param name = "elementId">The element ID.</param>
        /// <remarks>The hosting DataMiner Agent ID value will be set to the same value as the specified DataMiner Agent ID.</remarks>
        /// <exception cref = "ArgumentException"><paramref name = "agentId"/> is invalid.</exception>
        /// <exception cref = "ArgumentException"><paramref name = "elementId"/> is invalid.</exception>
        public DmsElementId(int agentId, int elementId)
        {
            if ((elementId == -1 && agentId != -1) || agentId < -1)
            {
                string message = System.String.Format(System.Globalization.CultureInfo.InvariantCulture, "Invalid agent ID. Value: {0}.", agentId);
                throw new System.ArgumentException(message, "agentId");
            }

            if ((agentId == -1 && elementId != -1) || elementId < -1)
            {
                string message = System.String.Format(System.Globalization.CultureInfo.InvariantCulture, "Invalid element ID. Value: {0}.", elementId);
                throw new System.ArgumentException(message, "elementId");
            }

            this.elementId = elementId;
            this.agentId = agentId;
        }

        /// <summary>
        /// Gets the DataMiner Agent ID.
        /// </summary>
        /// <remarks>The DataMiner Agent ID is the ID of the DataMiner Agent this element has been created on.</remarks>
        public int AgentId
        {
            get
            {
                return agentId;
            }
        }

        /// <summary>
        /// Gets the element ID.
        /// </summary>
        public int ElementId
        {
            get
            {
                return elementId;
            }
        }

        /// <summary>
        /// Gets the DataMiner Agent ID/element ID string representation.
        /// </summary>
        public string Value
        {
            get
            {
                return agentId + "/" + elementId;
            }
        }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the
        /// current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name = "other">An object to compare with this instance.</param>
        /// <returns>A value that indicates the relative order of the objects being compared.
        /// The return value has these meanings: Less than zero means this instance precedes <paramref name = "other"/> in the sort order.
        /// Zero means this instance occurs in the same position in the sort order as <paramref name = "other"/>.
        /// Greater than zero means this instance follows <paramref name = "other"/> in the sort order.</returns>
        /// <remarks>The order of the comparison is as follows: DataMiner Agent ID, element ID.</remarks>
        public int CompareTo(Skyline.DataMiner.Library.Common.DmsElementId other)
        {
            int result = agentId.CompareTo(other.AgentId);
            if (result == 0)
            {
                result = elementId.CompareTo(other.ElementId);
            }

            return result;
        }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name = "obj">An object to compare with this instance.</param>
        /// <returns>A value that indicates the relative order of the objects being compared. The return value has these meanings: Less than zero means this instance precedes <paramref name = "obj"/> in the sort order. Zero means this instance occurs in the same position in the sort order as <paramref name = "obj"/>. Greater than zero means this instance follows <paramref name = "obj"/> in the sort order.</returns>
        /// <remarks>The order of the comparison is as follows: DataMiner Agent ID, element ID.</remarks>
        /// <exception cref = "ArgumentException">The obj is not of type <see cref = "DmsElementId"/></exception>
        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            if (!(obj is Skyline.DataMiner.Library.Common.DmsElementId))
            {
                throw new System.ArgumentException("The provided object must be of type DmsElementId.", "obj");
            }

            return CompareTo((Skyline.DataMiner.Library.Common.DmsElementId)obj);
        }

        /// <summary>
        /// Compares the object to another object.
        /// </summary>
        /// <param name = "obj">The object to compare against.</param>
        /// <returns><c>true</c> if the elements are equal; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Skyline.DataMiner.Library.Common.DmsElementId))
            {
                return false;
            }

            return Equals((Skyline.DataMiner.Library.Common.DmsElementId)obj);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name = "other">An object to compare with this object.</param>
        /// <returns><c>true</c> if the elements are equal; otherwise, <c>false</c>.</returns>
        public bool Equals(Skyline.DataMiner.Library.Common.DmsElementId other)
        {
            if (elementId == other.elementId && agentId == other.agentId)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns the hash code.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
        {
            return elementId ^ agentId;
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return System.String.Format(System.Globalization.CultureInfo.InvariantCulture, "agent ID: {0}, element ID: {1}", agentId, elementId);
        }

        /// <summary>
        /// Returns a value determining whether the agent ID is valid.
        /// </summary>
        /// <returns><c>true</c> if the agent ID is valid; otherwise, <c>false</c>.</returns>
        private bool IsValidAgentId()
        {
            bool isValid = true;
            if ((elementId == -1 && agentId != -1) || agentId < -1)
            {
                isValid = false;
            }

            return isValid;
        }

        /// <summary>
        /// Returns a value determining whether the element ID is valid.
        /// </summary>
        /// <returns><c>true</c> if the element ID is valid; otherwise, <c>false</c>.</returns>
        private bool IsValidElementId()
        {
            bool isValid = true;
            if ((agentId == -1 && elementId != -1) || elementId < -1)
            {
                isValid = false;
            }

            return isValid;
        }
    }

    /// <summary>
    /// Represents a DataMiner System.
    /// </summary>
    internal class Dms : Skyline.DataMiner.Library.Common.IDms
    {
        /// <summary>
        /// Cached element information message.
        /// </summary>
        private Skyline.DataMiner.Net.Messages.ElementInfoEventMessage cachedElementInfoMessage;
        /// <summary>
        /// The object used for DMS communication.
        /// </summary>
        private Skyline.DataMiner.Library.Common.ICommunication communication;
        /// <summary>
        /// Initializes a new instance of the <see cref = "Dms"/> class.
        /// </summary>
        /// <param name = "communication">An object implementing the ICommunication interface.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "communication"/> is <see langword = "null"/>.</exception>
        internal Dms(Skyline.DataMiner.Library.Common.ICommunication communication)
        {
            if (communication == null)
            {
                throw new System.ArgumentNullException("communication");
            }

            this.communication = communication;
        }

        /// <summary>
        /// Gets the communication interface.
        /// </summary>
        /// <value>The communication interface.</value>
        public Skyline.DataMiner.Library.Common.ICommunication Communication
        {
            get
            {
                return communication;
            }
        }

        /// <summary>
        /// Determines whether an element with the specified Agent ID/element ID exists in the DataMiner System.
        /// </summary>
        /// <param name = "dmsElementId">The DataMiner Agent ID/element ID of the element.</param>
        /// <returns><c>true</c> if the element exists; otherwise, <c>false</c>.</returns>
        /// <exception cref = "ArgumentException"><paramref name = "dmsElementId"/> is invalid.</exception>
        public bool ElementExists(Skyline.DataMiner.Library.Common.DmsElementId dmsElementId)
        {
            int dmaId = dmsElementId.AgentId;
            int elementId = dmsElementId.ElementId;
            if (dmaId < 1)
            {
                throw new System.ArgumentException(System.String.Format(System.Globalization.CultureInfo.InvariantCulture, "Invalid DataMiner agent ID: {0}", dmaId), "dmsElementId");
            }

            if (elementId < 1)
            {
                throw new System.ArgumentException(System.String.Format(System.Globalization.CultureInfo.InvariantCulture, "Invalid DataMiner element ID: {0}", elementId), "dmsElementId");
            }

            try
            {
                Skyline.DataMiner.Net.Messages.GetElementByIDMessage message = new Skyline.DataMiner.Net.Messages.GetElementByIDMessage(dmaId, elementId);
                Skyline.DataMiner.Net.Messages.ElementInfoEventMessage response = (Skyline.DataMiner.Net.Messages.ElementInfoEventMessage)Communication.SendSingleResponseMessage(message);
                // Cache the response of SLNet.
                // Could be useful when this call is used within a "GetElement" this makes sure that we do not double call SLNet.
                if (response != null)
                {
                    cachedElementInfoMessage = response;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Skyline.DataMiner.Net.Exceptions.DataMinerException e)
            {
                if (e.ErrorCode == -2146233088)
                {
                    // 0x80131500, Element "[element name]" is unavailable.
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Retrieves the element with the specified ID.
        /// </summary>
        /// <param name = "dmsElementId">The DataMiner Agent ID/element ID of the element.</param>
        /// <exception cref = "ArgumentException"><paramref name = "dmsElementId"/> is invalid.</exception>
        /// <exception cref = "ElementNotFoundException">The element with the specified ID was not found in the DataMiner System.</exception>
        /// <returns>The element with the specified ID.</returns>
        public Skyline.DataMiner.Library.Common.IDmsElement GetElement(Skyline.DataMiner.Library.Common.DmsElementId dmsElementId)
        {
            if (!ElementExists(dmsElementId))
            {
                throw new Skyline.DataMiner.Library.Common.ElementNotFoundException(dmsElementId);
            }

            return new Skyline.DataMiner.Library.Common.DmsElement(this, cachedElementInfoMessage);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return "DataMiner System";
        }
    }

    /// <summary>
    /// Helper class to convert from enumeration value to string or vice versa.
    /// </summary>
    internal static class EnumMapper
    {
        /// <summary>
        /// The connection type map.
        /// </summary>
        private static readonly System.Collections.Generic.Dictionary<System.String, Skyline.DataMiner.Library.Common.ConnectionType> ConnectionTypeMapping = new System.Collections.Generic.Dictionary<System.String, Skyline.DataMiner.Library.Common.ConnectionType>{{"SNMP", Skyline.DataMiner.Library.Common.ConnectionType.SnmpV1}, {"SNMPV1", Skyline.DataMiner.Library.Common.ConnectionType.SnmpV1}, {"SNMPV2", Skyline.DataMiner.Library.Common.ConnectionType.SnmpV2}, {"SNMPV3", Skyline.DataMiner.Library.Common.ConnectionType.SnmpV3}, {"SERIAL", Skyline.DataMiner.Library.Common.ConnectionType.Serial}, {"SERIAL SINGLE", Skyline.DataMiner.Library.Common.ConnectionType.SerialSingle}, {"SMART-SERIAL", Skyline.DataMiner.Library.Common.ConnectionType.SmartSerial}, {"SMART-SERIAL SINGLE", Skyline.DataMiner.Library.Common.ConnectionType.SmartSerialSingle}, {"HTTP", Skyline.DataMiner.Library.Common.ConnectionType.Http}, {"GPIB", Skyline.DataMiner.Library.Common.ConnectionType.Gpib}, {"VIRTUAL", Skyline.DataMiner.Library.Common.ConnectionType.Virtual}, {"OPC", Skyline.DataMiner.Library.Common.ConnectionType.Opc}, {"SLA", Skyline.DataMiner.Library.Common.ConnectionType.Sla}, {"WEBSOCKET", Skyline.DataMiner.Library.Common.ConnectionType.WebSocket}};
        /// <summary>
        /// Converts a string denoting a connection type to the corresponding value of the <see cref = "ConnectionType"/> enumeration.
        /// </summary>
        /// <param name = "type">The connection type.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "type"/> is <see langword = "null"/>.</exception>
        /// <exception cref = "ArgumentException"><paramref name = "type"/> is the empty string ("") or white space</exception>
        /// <exception cref = "KeyNotFoundException"></exception>
        /// <returns>The corresponding <see cref = "ConnectionType"/> value.</returns>
        internal static Skyline.DataMiner.Library.Common.ConnectionType ConvertStringToConnectionType(string type)
        {
            if (type == null)
            {
                throw new System.ArgumentNullException("type");
            }

            if (System.String.IsNullOrWhiteSpace(type))
            {
                throw new System.ArgumentException("The type must not be empty.", "type");
            }

            string valueLower = type.ToUpperInvariant();
            Skyline.DataMiner.Library.Common.ConnectionType result;
            if (!ConnectionTypeMapping.TryGetValue(valueLower, out result))
            {
                throw new System.Collections.Generic.KeyNotFoundException(System.String.Format(System.Globalization.CultureInfo.InvariantCulture, "The key {0} could not be found.", valueLower));
            }

            return result;
        }
    }

    /// <summary>
    /// Class containing helper methods.
    /// </summary>
    internal static class HelperClass
    {
        /// <summary>
        /// Checks the element state and throws an exception if no operation is possible due to the current element state.
        /// </summary>
        /// <param name = "element">The element on which to check the state.</param>
        /// <exception cref = "ElementNotFoundException">The element was not found in the DataMiner system.</exception>
        /// <exception cref = "ElementStoppedException">The element is stopped.</exception>
        internal static void CheckElementState(Skyline.DataMiner.Library.Common.IDmsElement element)
        {
            if (element.State == Skyline.DataMiner.Library.Common.ElementState.Deleted)
            {
                throw new Skyline.DataMiner.Library.Common.ElementNotFoundException(System.String.Format(System.Globalization.CultureInfo.InvariantCulture, "Failed to perform an operation on the element: {0} because it has been deleted.", element.Name));
            }

            if (element.State == Skyline.DataMiner.Library.Common.ElementState.Stopped)
            {
                throw new Skyline.DataMiner.Library.Common.ElementStoppedException(System.String.Format(System.Globalization.CultureInfo.InvariantCulture, "Failed to perform an operation on the element: {0} because it has been stopped.", element.Name));
            }
        }

        /// <summary>
        /// Using the description attribute on an enum, we can easily find back a corresponding enum value.
        /// </summary>
        /// <typeparam name = "T"></typeparam>
        /// <param name = "description"></param>
        /// <returns></returns>
        internal static T GetEnumFromDescription<T>(string description)
        {
            var type = typeof(T);
            if (!type.IsEnum)
                throw new System.InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                var attribute = System.Attribute.GetCustomAttribute(field, typeof(System.ComponentModel.DescriptionAttribute)) as System.ComponentModel.DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }

            if (Skyline.DataMiner.Library.Common.SnmpV3SecurityConfig.IsMarkedAsCompatibilityChange(description))
            {
                return (T)(object)Skyline.DataMiner.Library.Common.SnmpV3SecurityConfig.GetCompatibleAuthenticationAlgorithmFromDMA(description);
            }

            throw new System.ArgumentException(description + "Not found as Enum.");
        }
    }

    /// <summary>
    /// DataMiner System interface.
    /// </summary>
    public interface IDms
    {
        /// <summary>
        /// Gets the communication interface.
        /// </summary>
        /// <value>The communication interface.</value>
        Skyline.DataMiner.Library.Common.ICommunication Communication
        {
            get;
        }

        /// <summary>
        /// Determines whether an element with the specified DataMiner Agent ID/element ID exists in the DataMiner System.
        /// </summary>
        /// <param name = "dmsElementId">The DataMiner Agent ID/element ID of the element.</param>
        /// <returns><c>true</c> if the element exists; otherwise, <c>false</c>.</returns>
        bool ElementExists(Skyline.DataMiner.Library.Common.DmsElementId dmsElementId);
        /// <summary>
        /// Retrieves the element with the specified element ID.
        /// </summary>
        /// <param name = "dmsElementId">The DataMiner Agent ID/element ID of the element.</param>
        /// <exception cref = "ArgumentException"><paramref name = "dmsElementId"/> is empty.</exception>
        /// <exception cref = "ElementNotFoundException">No element with the specified ID exists in the DataMiner System.</exception>
        /// <returns>The element with the specified ID.</returns>
        Skyline.DataMiner.Library.Common.IDmsElement GetElement(Skyline.DataMiner.Library.Common.DmsElementId dmsElementId);
    }

    /// <summary>
    /// Contains methods for input validation.
    /// </summary>
    internal static class InputValidator
    {
        /// <summary>
        /// Validates the name of an element, service, redundancy group, template or folder.
        /// </summary>
        /// <param name = "name">The element name.</param>
        /// <param name = "parameterName">The name of the parameter that is passing the name.</param>
        /// <exception cref = "ArgumentNullException">The value of a set operation is <see langword = "null"/>.</exception>
        /// <exception cref = "ArgumentException">The value of a set operation is empty or white space.</exception>
        /// <exception cref = "ArgumentException">The value of a set operation exceeds 200 characters.</exception>
        /// <exception cref = "ArgumentException">The value of a set operation contains a forbidden character.</exception>
        /// <exception cref = "ArgumentException">The value of a set operation contains more than one '%' character.</exception>
        /// <returns><c>true</c> if the name is valid; otherwise, <c>false</c>.</returns>
        /// <remarks>Forbidden characters: '\', '/', ':', '*', '?', '"', '&lt;', '&gt;', '|', '°', ';'.</remarks>
        public static string ValidateName(string name, string parameterName)
        {
            if (name == null)
            {
                throw new System.ArgumentNullException("name");
            }

            if (parameterName == null)
            {
                throw new System.ArgumentNullException("parameterName");
            }

            if (System.String.IsNullOrWhiteSpace(name))
            {
                throw new System.ArgumentException("The name must not be null or white space.", parameterName);
            }

            string trimmedName = name.Trim();
            if (trimmedName.Length > 200)
            {
                throw new System.ArgumentException("The name must not exceed 200 characters.", parameterName);
            }

            // White space is trimmed.
            if (trimmedName[0].Equals('.'))
            {
                throw new System.ArgumentException("The name must not start with a dot ('.').", parameterName);
            }

            if (trimmedName[trimmedName.Length - 1].Equals('.'))
            {
                throw new System.ArgumentException("The name must not end with a dot ('.').", parameterName);
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(trimmedName, @"^[^/\\:;\*\?<>\|°""]+$"))
            {
                throw new System.ArgumentException("The name contains a forbidden character.", parameterName);
            }

            if (System.Linq.Enumerable.Count(trimmedName, x => x == '%') > 1)
            {
                throw new System.ArgumentException("The name must not contain more than one '%' characters.", parameterName);
            }

            return trimmedName;
        }
    }

    /// <summary>
    /// Updateable interface.
    /// </summary>
    public interface IUpdateable
    {
    }

    /// <summary>
    /// Represents a DataMiner Agent.
    /// </summary>
    internal class Dma : Skyline.DataMiner.Library.Common.DmsObject, Skyline.DataMiner.Library.Common.IDma
    {
        /// <summary>
        /// The object used for DMS communication.
        /// </summary>
        private new readonly Skyline.DataMiner.Library.Common.IDms dms;
        /// <summary>
        /// The DataMiner Agent ID.
        /// </summary>
        private readonly int id;
        private string versionInfo;
        private string hostName;
        private string name;
        /// <summary>
        /// Initializes a new instance of the <see cref = "Dma"/> class.
        /// </summary>
        /// <param name = "dms">The DataMiner System.</param>
        /// <param name = "id">The ID of the DataMiner Agent.</param>
        /// <exception cref = "ArgumentNullException">The <see cref = "IDms"/> reference is <see langword = "null"/>.</exception>
        /// <exception cref = "ArgumentException">The DataMiner Agent ID is negative.</exception>
        internal Dma(Skyline.DataMiner.Library.Common.IDms dms, int id): base(dms)
        {
            if (id < 1)
            {
                throw new System.ArgumentException(System.String.Format(System.Globalization.CultureInfo.InvariantCulture, "Invalid DataMiner agent ID: {0}", id), "id");
            }

            this.dms = dms;
            this.id = id;
        }

        internal Dma(Skyline.DataMiner.Library.Common.IDms dms, Skyline.DataMiner.Net.Messages.GetDataMinerInfoResponseMessage infoMessage): base(dms)
        {
            if (infoMessage == null)
            {
                throw new System.ArgumentNullException("infoMessage");
            }

            Parse(infoMessage);
        }

        /// <summary>
        /// Gets the ID of this DataMiner Agent.
        /// </summary>
        /// <value>The ID of this DataMiner Agent.</value>
        public int Id
        {
            get
            {
                return id;
            }
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return System.String.Format(System.Globalization.CultureInfo.InvariantCulture, "DataMiner agent ID: {0}", id);
        }

        internal override void Load()
        {
            try
            {
                Skyline.DataMiner.Net.Messages.GetDataMinerByIDMessage message = new Skyline.DataMiner.Net.Messages.GetDataMinerByIDMessage(id);
                Skyline.DataMiner.Net.Messages.GetDataMinerInfoResponseMessage infoResponseMessage = Dms.Communication.SendSingleResponseMessage(message) as Skyline.DataMiner.Net.Messages.GetDataMinerInfoResponseMessage;
                if (infoResponseMessage != null)
                {
                    Parse(infoResponseMessage);
                }
                else
                {
                    throw new Skyline.DataMiner.Library.Common.AgentNotFoundException(id);
                }

                Skyline.DataMiner.Net.Messages.GetAgentBuildInfo buildInfoMessage = new Skyline.DataMiner.Net.Messages.GetAgentBuildInfo(id);
                Skyline.DataMiner.Net.Messages.BuildInfoResponse buildInfoResponse = (Skyline.DataMiner.Net.Messages.BuildInfoResponse)Dms.Communication.SendSingleResponseMessage(buildInfoMessage);
                if (buildInfoResponse != null)
                {
                    Parse(buildInfoResponse);
                }

                Skyline.DataMiner.Net.Messages.RSAPublicKeyRequest rsapkr;
                rsapkr = new Skyline.DataMiner.Net.Messages.RSAPublicKeyRequest(id)
                {HostingDataMinerID = id};
                Skyline.DataMiner.Net.Messages.RSAPublicKeyResponse resp = Dms.Communication.SendSingleResponseMessage(rsapkr) as Skyline.DataMiner.Net.Messages.RSAPublicKeyResponse;
                Skyline.DataMiner.Library.Common.RSA.PublicKey = new System.Security.Cryptography.RSAParameters{Modulus = resp.Modulus, Exponent = resp.Exponent};
                IsLoaded = true;
            }
            catch (Skyline.DataMiner.Net.Exceptions.DataMinerException e)
            {
                if (e.ErrorCode == -2146233088)
                {
                    // 0x80131500, No agent available with ID.
                    throw new Skyline.DataMiner.Library.Common.AgentNotFoundException(id);
                }
                else
                {
                    throw;
                }
            }
        }

        private void Parse(Skyline.DataMiner.Net.Messages.GetDataMinerInfoResponseMessage infoMessage)
        {
            name = infoMessage.AgentName;
            hostName = infoMessage.ComputerName;
        }

        /// <summary>
        /// Parses the version information of the DataMiner Agent.
        /// </summary>
        /// <param name = "response">The <see cref = "BuildInfoResponse"/> object to be parsed.</param>
        /// <exception cref = "ArgumentException">Agent build information cannot be null or empty.</exception>
        private void Parse(Skyline.DataMiner.Net.Messages.BuildInfoResponse response)
        {
            if (response == null || response.Agents == null || response.Agents.Length == 0)
            {
                throw new System.ArgumentException("Agent build information cannot be null or empty");
            }

            string rawVersion = response.Agents[0].RawVersion;
            this.versionInfo = rawVersion;
        }
    }

    /// <summary>
    /// DataMiner Agent interface.
    /// </summary>
    public interface IDma
    {
        /// <summary>
        /// Gets the DataMiner System this Agent is part of.
        /// </summary>
        /// <value>The DataMiner system this Agent is part of.</value>
        Skyline.DataMiner.Library.Common.IDms Dms
        {
            get;
        }

        /// <summary>
        /// Gets the ID of this DataMiner Agent.
        /// </summary>
        /// <value>The ID of this DataMiner Agent.</value>
        int Id
        {
            get;
        }
    }

    /// <summary>
    /// Represents a communication interface implementation using the <see cref = "IConnection"/> interface.
    /// </summary>
    internal class ConnectionCommunication : Skyline.DataMiner.Library.Common.ICommunication
    {
        /// <summary>
        /// The SLNet connection.
        /// </summary>
        private readonly Skyline.DataMiner.Net.IConnection connection;
        /// <summary>
        /// Initializes a new instance of the <see cref = "ConnectionCommunication"/> class using an instance of the <see cref = "IConnection"/> class.
        /// </summary>
        /// <param name = "connection">The connection.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "connection"/> is <see langword = "null"/>.</exception>
        public ConnectionCommunication(Skyline.DataMiner.Net.IConnection connection)
        {
            if (connection == null)
            {
                throw new System.ArgumentNullException("connection");
            }

            this.connection = connection;
        }

        /// <summary>
        /// Sends a message to the SLNet process.
        /// </summary>
        /// <param name = "message">The message to be sent.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "message"/> is <see langword = "null"/>.</exception>
        /// <returns>The message responses.</returns>
        public Skyline.DataMiner.Net.Messages.DMSMessage[] SendMessage(Skyline.DataMiner.Net.Messages.DMSMessage message)
        {
            if (message == null)
            {
                throw new System.ArgumentNullException("message");
            }

            return connection.HandleMessage(message);
        }

        /// <summary>
        /// Sends a message to the SLNet process.
        /// </summary>
        /// <param name = "message">The message to be sent.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "message"/> is <see langword = "null"/>.</exception>
        /// <returns>The message response.</returns>
        public Skyline.DataMiner.Net.Messages.DMSMessage SendSingleResponseMessage(Skyline.DataMiner.Net.Messages.DMSMessage message)
        {
            if (message == null)
            {
                throw new System.ArgumentNullException("message");
            }

            return connection.HandleSingleResponseMessage(message);
        }
    }

    /// <summary>
    /// Defines methods to send messages to a DataMiner System.
    /// </summary>
    public interface ICommunication
    {
        /// <summary>
        /// Sends a message to the SLNet process that can have multiple responses.
        /// </summary>
        /// <param name = "message">The message to be sent.</param>
        /// <exception cref = "ArgumentNullException">The message cannot be null.</exception>
        /// <returns>The message responses.</returns>
        Skyline.DataMiner.Net.Messages.DMSMessage[] SendMessage(Skyline.DataMiner.Net.Messages.DMSMessage message);
        /// <summary>
        /// Sends a message to the SLNet process that returns a single response.
        /// </summary>
        /// <param name = "message">The message to be sent.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "message"/> is <see langword = "null"/>.</exception>
        /// <returns>The message response.</returns>
        Skyline.DataMiner.Net.Messages.DMSMessage SendSingleResponseMessage(Skyline.DataMiner.Net.Messages.DMSMessage message);
    }

    /// <summary>
    /// A collection of IElementConnection objects.
    /// </summary>
    public class ElementConnectionCollection
    {
        private readonly Skyline.DataMiner.Library.Common.IElementConnection[] connections;
        private readonly bool canBeValidated;
        private readonly System.Collections.Generic.IList<Skyline.DataMiner.Library.Common.IDmsConnectionInfo> protocolConnectionInfo;
        /// <summary>
        /// initiates a new instance.
        /// </summary>
        /// <param name = "protocolConnectionInfo"></param>
        internal ElementConnectionCollection(System.Collections.Generic.IList<Skyline.DataMiner.Library.Common.IDmsConnectionInfo> protocolConnectionInfo)
        {
            int amountOfConnections = protocolConnectionInfo.Count;
            this.connections = new Skyline.DataMiner.Library.Common.IElementConnection[amountOfConnections];
            this.protocolConnectionInfo = protocolConnectionInfo;
            canBeValidated = true;
        }

        /// <summary>
        /// Initiates a new instance.
        /// </summary>
        /// <param name = "message"></param>
        internal ElementConnectionCollection(Skyline.DataMiner.Net.Messages.ElementInfoEventMessage message)
        {
            int amountOfConnections = 1;
            if (message != null && message.ExtraPorts != null)
            {
                amountOfConnections += message.ExtraPorts.Length;
            }

            this.connections = new Skyline.DataMiner.Library.Common.IElementConnection[amountOfConnections];
            canBeValidated = false;
        }

        /// <summary>
        /// Gets or sets an entry in the collection.
        /// </summary>
        /// <param name = "index"></param>
        /// <returns></returns>
        public IElementConnection this[int index]
        {
            get
            {
                return connections[index];
            }

            set
            {
                bool valid = ValidateConnectionTypeAtPos(index, value);
                if (valid)
                {
                    connections[index] = value;
                }
                else
                {
                    throw new Skyline.DataMiner.Library.Common.IncorrectDataException("Invalid connection type provided at index " + index);
                }
            }
        }

        /// <summary>
        /// Validates the provided <see cref = "IElementConnection"/> against the provided Protocol.
        /// </summary>
        /// <param name = "index">The index position of the connection to validate.</param>
        /// <param name = "conn">The IElementConnection connection.</param>
        /// <exception cref = "ArgumentOutOfRangeException"><paramref name = "index"/> is out of range.</exception>
        /// <returns></returns>
        private bool ValidateConnectionTypeAtPos(int index, Skyline.DataMiner.Library.Common.IElementConnection conn)
        {
            if (!canBeValidated)
            {
                return true;
            }

            if (index < 0 || ((index + 1) > protocolConnectionInfo.Count))
            {
                throw new System.ArgumentOutOfRangeException("index", "Index needs to be between 0 and the amount of connections in the protocol minus 1.");
            }

            return ValidateConnectionInfo(conn, protocolConnectionInfo[index]);
        }

        /// <summary>
        /// Validates a single connection.
        /// </summary>
        /// <param name = "conn"><see cref = "IElementConnection"/> object.</param>
        /// <param name = "connectionInfo"><see cref = "IDmsConnectionInfo"/> object.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "conn"/> is <see langword = "null"/>.</exception>
        /// <exception cref = "ArgumentNullException"><paramref name = "connectionInfo"/> is <see langword = "null"/>.</exception>
        /// <returns></returns>
        private static bool ValidateConnectionInfo(Skyline.DataMiner.Library.Common.IElementConnection conn, Skyline.DataMiner.Library.Common.IDmsConnectionInfo connectionInfo)
        {
            if (conn == null)
            {
                throw new Skyline.DataMiner.Library.Common.IncorrectDataException("conn: Invalid data , ElementConfiguration does not contain connection info");
            }

            if (connectionInfo == null)
            {
                throw new Skyline.DataMiner.Library.Common.IncorrectDataException("connectionInfo: Invalid data , Protocol does not contain connection info");
            }

            switch (connectionInfo.Type)
            {
                case Skyline.DataMiner.Library.Common.ConnectionType.SnmpV1:
                    return ValidateAsSnmpV1(conn);
                case Skyline.DataMiner.Library.Common.ConnectionType.SnmpV2:
                    return ValidateAsSnmpV2(conn);
                case Skyline.DataMiner.Library.Common.ConnectionType.SnmpV3:
                    return ValidateAsSnmpV3(conn);
                case Skyline.DataMiner.Library.Common.ConnectionType.Virtual:
                    return conn is Skyline.DataMiner.Library.Common.IVirtualConnection;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Validate a connection for SNMPv1
        /// </summary>
        /// <param name = "conn">object of type <see cref = "IElementConnection"/> to validate.</param>
        /// <returns></returns>
        private static bool ValidateAsSnmpV1(Skyline.DataMiner.Library.Common.IElementConnection conn)
        {
            return conn is Skyline.DataMiner.Library.Common.ISnmpV1Connection || conn is Skyline.DataMiner.Library.Common.ISnmpV2Connection || conn is Skyline.DataMiner.Library.Common.ISnmpV3Connection;
        }

        /// <summary>
        /// Validate a connection for SNMPv2
        /// </summary>
        /// <param name = "conn">object of type <see cref = "IElementConnection"/> to validate.</param>
        /// <returns></returns>
        private static bool ValidateAsSnmpV2(Skyline.DataMiner.Library.Common.IElementConnection conn)
        {
            return conn is Skyline.DataMiner.Library.Common.ISnmpV2Connection || conn is Skyline.DataMiner.Library.Common.ISnmpV3Connection;
        }

        /// <summary>
        /// Validate a connection for SNMPv3
        /// </summary>
        /// <param name = "conn">object of type <see cref = "IElementConnection"/> to validate.</param>
        /// <returns></returns>
        private static bool ValidateAsSnmpV3(Skyline.DataMiner.Library.Common.IElementConnection conn)
        {
            return conn is Skyline.DataMiner.Library.Common.ISnmpV3Connection || conn is Skyline.DataMiner.Library.Common.ISnmpV2Connection;
        }
    }

    /// <summary>
    /// Represents information about a connection.
    /// </summary>
    internal class DmsConnectionInfo : Skyline.DataMiner.Library.Common.IDmsConnectionInfo
    {
        /// <summary>
        /// The name of the connection.
        /// </summary>
        private readonly string name;
        /// <summary>
        /// The connection type.
        /// </summary>
        private readonly Skyline.DataMiner.Library.Common.ConnectionType type;
        /// <summary>
        /// Initializes a new instance of the <see cref = "DmsConnectionInfo"/> class.
        /// </summary>
        /// <param name = "name">The connection name.</param>
        /// <param name = "type">The connection type.</param>
        internal DmsConnectionInfo(string name, Skyline.DataMiner.Library.Common.ConnectionType type)
        {
            this.name = name;
            this.type = type;
        }

        /// <summary>
        /// Gets the connection type.
        /// </summary>
        /// <value>The connection type.</value>
        public Skyline.DataMiner.Library.Common.ConnectionType Type
        {
            get
            {
                return type;
            }
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return System.String.Format(System.Globalization.CultureInfo.InvariantCulture, "Connection with Name:{0} and Type:{1}.", name, type);
        }
    }

    /// <summary>
    /// DataMiner element connection information interface.
    /// </summary>
    public interface IDmsConnectionInfo
    {
        /// <summary>
        /// Gets the connection type.
        /// </summary>
        /// <value>The connection type.</value>
        Skyline.DataMiner.Library.Common.ConnectionType Type
        {
            get;
        }
    }

    /// <summary>
    /// Specifies the connection type.
    /// </summary>
    public enum ConnectionType
    {
        /// <summary>
        /// Undefined connection type.
        /// </summary>
        Undefined = 0,
        /// <summary>
        /// SNMPv1 connection.
        /// </summary>
        SnmpV1 = 1,
        /// <summary>
        /// Serial connection.
        /// </summary>
        Serial = 2,
        /// <summary>
        /// Smart-serial connection.
        /// </summary>
        SmartSerial = 3,
        /// <summary>
        /// Virtual connection.
        /// </summary>
        Virtual = 4,
        /// <summary>
        /// GBIP (General Purpose Interface Bus) connection.
        /// </summary>
        Gpib = 5,
        /// <summary>
        /// OPC (OLE for Process Control) connection.
        /// </summary>
        Opc = 6,
        /// <summary>
        /// SLA (Service Level Agreement).
        /// </summary>
        Sla = 7,
        /// <summary>
        /// SNMPv2 connection.
        /// </summary>
        SnmpV2 = 8,
        /// <summary>
        /// SNMPv3 connection.
        /// </summary>
        SnmpV3 = 9,
        /// <summary>
        /// HTTP connection.
        /// </summary>
        Http = 10,
        /// <summary>
        /// Service.
        /// </summary>
        Service = 11,
        /// <summary>
        /// Serial single connection.
        /// </summary>
        SerialSingle = 12,
        /// <summary>
        /// Smart-serial single connection.
        /// </summary>
        SmartSerialSingle = 13,
        /// <summary>
        /// Web Socket connection.
        /// </summary>
        WebSocket = 14
    }

    /// <summary>
    /// Specifies the state of the element.
    /// </summary>
    public enum ElementState
    {
        /// <summary>
        /// Specifies the undefined element state.
        /// </summary>
        Undefined = 0,
        /// <summary>
        /// Specifies the active element state.
        /// </summary>
        Active = 1,
        /// <summary>
        /// Specifies the hidden element state.
        /// </summary>
        Hidden = 2,
        /// <summary>
        /// Specifies the paused element state.
        /// </summary>
        Paused = 3,
        /// <summary>
        /// Specifies the stopped element state.
        /// </summary>
        Stopped = 4,
        /// <summary>
        /// Specifies the deleted element state.
        /// </summary>
        Deleted = 6,
        /// <summary>
        /// Specifies the error element state.
        /// </summary>
        Error = 10,
        /// <summary>
        /// Specifies the restart element state.
        /// </summary>
        Restart = 11,
        /// <summary>
        /// Specifies the masked element state.
        /// </summary>
        Masked = 12
    }

    /// <summary>
    /// The exception that is thrown when an action is performed on a DataMiner Agent that was not found.
    /// </summary>
    [System.Serializable]
    [Skyline.DataMiner.Library.Common.Attributes.DllImport("System.Runtime.Serialization.dll")]
    public class AgentNotFoundException : Skyline.DataMiner.Library.Common.DmsException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref = "AgentNotFoundException"/> class.
        /// </summary>
        public AgentNotFoundException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref = "AgentNotFoundException"/> class with a specified DataMiner Agent ID.
        /// </summary>
        /// <param name = "id">The ID of the DataMiner Agent that was not found.</param>
        public AgentNotFoundException(int id): base(System.String.Format(System.Globalization.CultureInfo.InvariantCulture, "The agent with ID '{0}' was not found.", id))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref = "AgentNotFoundException"/> class with a specified error message.
        /// </summary>
        /// <param name = "message">The error message that explains the reason for the exception.</param>
        public AgentNotFoundException(string message): base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref = "AgentNotFoundException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name = "message">The error message that explains the reason for the exception.</param>
        /// <param name = "innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public AgentNotFoundException(string message, System.Exception innerException): base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref = "AgentNotFoundException"/> class with serialized data.
        /// </summary>
        /// <param name = "info">The serialization info.</param>
        /// <param name = "context">The streaming context.</param>
        /// <exception cref = "ArgumentNullException">The <paramref name = "info"/> parameter is <see langword = "null"/>.</exception>
        /// <exception cref = "SerializationException">The class name is <see langword = "null"/> or HResult is zero (0).</exception>
        /// <remarks>This constructor is called during deserialization to reconstitute the exception object transmitted over a stream.</remarks>
        protected AgentNotFoundException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context): base(info, context)
        {
        }
    }

    /// <summary>
    /// The exception that is thrown when a requested alarm template was not found.
    /// </summary>
    [System.Serializable]
    [Skyline.DataMiner.Library.Common.Attributes.DllImport("System.Runtime.Serialization.dll")]
    public class AlarmTemplateNotFoundException : Skyline.DataMiner.Library.Common.TemplateNotFoundException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref = "AlarmTemplateNotFoundException"/> class.
        /// </summary>
        public AlarmTemplateNotFoundException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref = "AlarmTemplateNotFoundException"/> class.
        /// </summary>
        /// <param name = "message">The error message that explains the reason for the exception.</param>
        public AlarmTemplateNotFoundException(string message): base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref = "AlarmTemplateNotFoundException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name = "message">The error message that explains the reason for the exception.</param>
        /// <param name = "innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public AlarmTemplateNotFoundException(string message, System.Exception innerException): base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref = "AlarmTemplateNotFoundException"/> class.
        /// </summary>
        /// <param name = "templateName">The name of the template.</param>
        /// <param name = "protocol">The protocol this template relates to.</param>
        public AlarmTemplateNotFoundException(string templateName, Skyline.DataMiner.Library.Common.IDmsProtocol protocol): base(templateName, protocol)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref = "AlarmTemplateNotFoundException"/> class.
        /// </summary>
        /// <param name = "templateName">The name of the template.</param>
        /// <param name = "protocolName">The name of the protocol.</param>
        /// <param name = "protocolVersion">The version of the protocol.</param>
        public AlarmTemplateNotFoundException(string templateName, string protocolName, string protocolVersion): base(templateName, protocolName, protocolVersion)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref = "AlarmTemplateNotFoundException"/> class with serialized data.
        /// </summary>
        /// <param name = "info">The serialization info.</param>
        /// <param name = "context">The streaming context.</param>
        /// <exception cref = "ArgumentNullException">The <paramref name = "info"/> parameter is <see langword = "null"/>.</exception>
        /// <exception cref = "SerializationException">The class name is <see langword = "null"/> or HResult is zero (0).</exception>
        /// <remarks>This constructor is called during deserialization to reconstitute the exception object transmitted over a stream.</remarks>
        protected AlarmTemplateNotFoundException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context): base(info, context)
        {
        }
    }

    /// <summary>
    /// The exception that is thrown when an exception occurs in a DataMiner System.
    /// </summary>
    [System.Serializable]
    [Skyline.DataMiner.Library.Common.Attributes.DllImport("System.Runtime.Serialization.dll")]
    public class DmsException : System.Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref = "DmsException"/> class.
        /// </summary>
        public DmsException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref = "DmsException"/> class.
        /// </summary>
        /// <param name = "message">The error message that explains the reason for the exception.</param>
        public DmsException(string message): base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref = "DmsException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name = "message">The error message that explains the reason for the exception.</param>
        /// <param name = "innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public DmsException(string message, System.Exception innerException): base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref = "DmsException"/> class with serialized data.
        /// </summary>
        /// <param name = "info">The serialization info.</param>
        /// <param name = "context">The streaming context.</param>
        /// <exception cref = "ArgumentNullException">The <paramref name = "info"/> parameter is <see langword = "null"/>.</exception>
        /// <exception cref = "SerializationException">The class name is <see langword = "null"/> or HResult is zero (0).</exception>
        /// <remarks>This constructor is called during deserialization to reconstitute the exception object transmitted over a stream.</remarks>
        protected DmsException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context): base(info, context)
        {
        }
    }

    /// <summary>
    /// The exception that is thrown when performing actions on an element that was not found.
    /// </summary>
    [System.Serializable]
    [Skyline.DataMiner.Library.Common.Attributes.DllImport("System.Runtime.Serialization.dll")]
    public class ElementNotFoundException : Skyline.DataMiner.Library.Common.DmsException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref = "ElementNotFoundException"/> class.
        /// </summary>
        public ElementNotFoundException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref = "ElementNotFoundException"/> class.
        /// </summary>
        /// <param name = "dmsElementId">The DataMiner Agent ID/element ID of the element that was not found.</param>
        public ElementNotFoundException(Skyline.DataMiner.Library.Common.DmsElementId dmsElementId): base(System.String.Format(System.Globalization.CultureInfo.InvariantCulture, "Element with DMA ID '{0}' and element ID '{1}' was not found.", dmsElementId.AgentId, dmsElementId.ElementId))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref = "ElementNotFoundException"/> class.
        /// </summary>
        /// <param name = "dmaId">The ID of the DataMiner Agent that was not found.</param>
        /// <param name = "elementId">The ID of the element that was not found.</param>
        public ElementNotFoundException(int dmaId, int elementId): base(System.String.Format(System.Globalization.CultureInfo.InvariantCulture, "Element with DMA ID '{0}' and element ID '{1}' was not found.", dmaId, elementId))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref = "ElementNotFoundException"/> class.
        /// </summary>
        /// <param name = "message">The error message that explains the reason for the exception.</param>
        public ElementNotFoundException(string message): base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref = "ElementNotFoundException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name = "message">The error message that explains the reason for the exception.</param>
        /// <param name = "innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public ElementNotFoundException(string message, System.Exception innerException): base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref = "ElementNotFoundException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name = "dmsElementId">The DataMiner Agent ID/element ID of the element that was not found.</param>
        /// <param name = "innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public ElementNotFoundException(Skyline.DataMiner.Library.Common.DmsElementId dmsElementId, System.Exception innerException): base(System.String.Format(System.Globalization.CultureInfo.InvariantCulture, "Element with DMA ID '{0}' and element ID '{1}' was not found.", dmsElementId.AgentId, dmsElementId.ElementId), innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref = "ElementNotFoundException"/> class with serialized data.
        /// </summary>
        /// <param name = "info">The serialization info.</param>
        /// <param name = "context">The streaming context.</param>
        /// <exception cref = "ArgumentNullException">The <paramref name = "info"/> parameter is <see langword = "null"/>.</exception>
        /// <exception cref = "SerializationException">The class name is <see langword = "null"/> or HResult is zero (0).</exception>
        /// <remarks>This constructor is called during deserialization to reconstitute the exception object transmitted over a stream.</remarks>
        protected ElementNotFoundException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context): base(info, context)
        {
        }
    }

    /// <summary>
    /// The exception that is thrown when an operation is performed on a stopped element.
    /// </summary>
    [System.Serializable]
    [Skyline.DataMiner.Library.Common.Attributes.DllImport("System.Runtime.Serialization.dll")]
    public class ElementStoppedException : Skyline.DataMiner.Library.Common.DmsException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref = "ElementStoppedException"/> class.
        /// </summary>
        public ElementStoppedException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref = "ElementStoppedException"/> class.
        /// </summary>
        /// <param name = "message">The error message that explains the reason for the exception.</param>
        public ElementStoppedException(string message): base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref = "ElementStoppedException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name = "dmsElementId">The ID of the element.</param>
        /// <param name = "innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public ElementStoppedException(Skyline.DataMiner.Library.Common.DmsElementId dmsElementId, System.Exception innerException): base(System.String.Format(System.Globalization.CultureInfo.InvariantCulture, "The element with ID '{0}' is stopped.", dmsElementId.Value), innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref = "ElementStoppedException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name = "message">The error message that explains the reason for the exception.</param>
        /// <param name = "innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public ElementStoppedException(string message, System.Exception innerException): base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref = "ElementStoppedException"/> class with serialized data.
        /// </summary>
        /// <param name = "info">The serialization info.</param>
        /// <param name = "context">The streaming context.</param>
        /// <exception cref = "ArgumentNullException">The <paramref name = "info"/> parameter is <see langword = "null"/>.</exception>
        /// <exception cref = "SerializationException">The class name is <see langword = "null"/> or HResult is zero (0).</exception>
        /// <remarks>This constructor is called during deserialization to reconstitute the exception object transmitted over a stream.</remarks>
        protected ElementStoppedException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context): base(info, context)
        {
        }
    }

    /// <summary>
    /// The exception that is thrown when invalid data was provided.
    /// </summary>
    [System.Serializable]
    [Skyline.DataMiner.Library.Common.Attributes.DllImport("System.Runtime.Serialization.dll")]
    public class IncorrectDataException : Skyline.DataMiner.Library.Common.DmsException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref = "IncorrectDataException"/> class.
        /// </summary>
        public IncorrectDataException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref = "IncorrectDataException"/> class.
        /// </summary>
        /// <param name = "message">The error message that explains the reason for the exception.</param>
        public IncorrectDataException(string message): base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref = "IncorrectDataException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name = "message">The error message that explains the reason for the exception.</param>
        /// <param name = "innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public IncorrectDataException(string message, System.Exception innerException): base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref = "IncorrectDataException"/> class with serialized data.
        /// </summary>
        /// <param name = "info">The serialization info.</param>
        /// <param name = "context">The streaming context.</param>
        /// <exception cref = "ArgumentNullException">The <paramref name = "info"/> parameter is <see langword = "null"/>.</exception>
        /// <exception cref = "SerializationException">The class name is <see langword = "null"/> or HResult is zero (0).</exception>
        /// <remarks>This constructor is called during deserialization to reconstitute the exception object transmitted over a stream.</remarks>
        protected IncorrectDataException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context): base(info, context)
        {
        }
    }

    /// <summary>
    /// The exception that is thrown when a requested protocol was not found.
    /// </summary>
    [System.Serializable]
    [Skyline.DataMiner.Library.Common.Attributes.DllImport("System.Runtime.Serialization.dll")]
    public class ProtocolNotFoundException : Skyline.DataMiner.Library.Common.DmsException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref = "ProtocolNotFoundException"/> class.
        /// </summary>
        public ProtocolNotFoundException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref = "ProtocolNotFoundException"/> class.
        /// </summary>
        /// <param name = "protocolName">The name of the protocol.</param>
        /// <param name = "protocolVersion">The version of the protocol.</param>
        public ProtocolNotFoundException(string protocolName, string protocolVersion): base(System.String.Format(System.Globalization.CultureInfo.InvariantCulture, "Protocol with name '{0}' and version '{1}' was not found.", protocolName, protocolVersion))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref = "ProtocolNotFoundException"/> class.
        /// </summary>
        /// <param name = "message">The error message that explains the reason for the exception.</param>
        public ProtocolNotFoundException(string message): base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref = "ProtocolNotFoundException"/> class.
        /// </summary>
        /// <param name = "protocolName">The name of the protocol.</param>
        /// <param name = "protocolVersion">The version of the protocol.</param>
        /// <param name = "innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public ProtocolNotFoundException(string protocolName, string protocolVersion, System.Exception innerException): base(System.String.Format(System.Globalization.CultureInfo.InvariantCulture, "Protocol with name '{0}' and version '{1}' was not found.", protocolName, protocolVersion), innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref = "ProtocolNotFoundException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name = "message">The error message that explains the reason for the exception.</param>
        /// <param name = "innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public ProtocolNotFoundException(string message, System.Exception innerException): base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref = "ProtocolNotFoundException"/> class with serialized data.
        /// </summary>
        /// <param name = "info">The serialization info.</param>
        /// <param name = "context">The streaming context.</param>
        /// <exception cref = "ArgumentNullException">The <paramref name = "info"/> parameter is <see langword = "null"/>.</exception>
        /// <exception cref = "SerializationException">The class name is <see langword = "null"/> or HResult is zero (0).</exception>
        /// <remarks>This constructor is called during deserialization to reconstitute the exception object transmitted over a stream.</remarks>
        protected ProtocolNotFoundException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context): base(info, context)
        {
        }
    }

    /// <summary>
    /// The exception that is thrown when a requested template was not found.
    /// </summary>
    [System.Serializable]
    [Skyline.DataMiner.Library.Common.Attributes.DllImport("System.Runtime.Serialization.dll")]
    public class TemplateNotFoundException : Skyline.DataMiner.Library.Common.DmsException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref = "TemplateNotFoundException"/> class.
        /// </summary>
        public TemplateNotFoundException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref = "TemplateNotFoundException"/> class.
        /// </summary>
        /// <param name = "templateName">The name of the template.</param>
        /// <param name = "protocol">The protocol this template relates to.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "protocol"/> is <see langword = "null"/>.</exception>
        public TemplateNotFoundException(string templateName, Skyline.DataMiner.Library.Common.IDmsProtocol protocol): base(BuildMessageString(templateName, protocol))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref = "TemplateNotFoundException"/> class.
        /// </summary>
        /// <param name = "templateName">The name of the template.</param>
        /// <param name = "protocolName">The name of the protocol.</param>
        /// <param name = "protocolVersion">The version of the protocol.</param>
        public TemplateNotFoundException(string templateName, string protocolName, string protocolVersion): base(System.String.Format(System.Globalization.CultureInfo.InvariantCulture, "Template \"{0}\" for protocol \"{1}\" version \"{2}\" was not found.", templateName, protocolName, protocolVersion))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref = "TemplateNotFoundException"/> class.
        /// </summary>
        /// <param name = "message">The error message that explains the reason for the exception.</param>
        public TemplateNotFoundException(string message): base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref = "TemplateNotFoundException"/> class.
        /// </summary>
        /// <param name = "templateName">The name of the template.</param>
        /// <param name = "protocolName">The name of the protocol.</param>
        /// <param name = "protocolVersion">The version of the protocol.</param>
        /// <param name = "innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public TemplateNotFoundException(string templateName, string protocolName, string protocolVersion, System.Exception innerException): base(System.String.Format(System.Globalization.CultureInfo.InvariantCulture, "Template \"{0}\" for protocol \"{1}\" version \"{2}\" was not found.", templateName, protocolName, protocolVersion), innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref = "TemplateNotFoundException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name = "message">The error message that explains the reason for the exception.</param>
        /// <param name = "innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public TemplateNotFoundException(string message, System.Exception innerException): base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref = "TemplateNotFoundException"/> class with serialized data.
        /// </summary>
        /// <param name = "info">The serialization info.</param>
        /// <param name = "context">The streaming context.</param>
        /// <exception cref = "ArgumentNullException">The <paramref name = "info"/> parameter is <see langword = "null"/>.</exception>
        /// <exception cref = "SerializationException">The class name is <see langword = "null"/> or HResult is zero (0).</exception>
        /// <remarks>This constructor is called during deserialization to reconstitute the exception object transmitted over a stream.</remarks>
        protected TemplateNotFoundException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context): base(info, context)
        {
        }

        private static string BuildMessageString(string templateName, Skyline.DataMiner.Library.Common.IDmsProtocol protocol)
        {
            if (protocol == null)
            {
                throw new System.ArgumentNullException("protocol");
            }

            return System.String.Format(System.Globalization.CultureInfo.InvariantCulture, "Template \"{0}\" for protocol \"{1}\" version \"{2}\" was not found.", templateName, protocol.Name, protocol.Version);
        }
    }

    /// <summary>
    /// Represents the parent for every type of object that can be present on a DataMiner system.
    /// </summary>
    internal abstract class DmsObject
    {
        /// <summary>
        /// The DataMiner system the object belongs to.
        /// </summary>
        protected readonly Skyline.DataMiner.Library.Common.IDms dms;
        /// <summary>
        /// Flag stating whether the DataMiner system object has been loaded.
        /// </summary>
        private bool isLoaded;
        /// <summary>
        /// Initializes a new instance of the <see cref = "DmsObject"/> class.
        /// </summary>
        /// <param name = "dms">Object implementing the <see cref = "IDms"/> interface.</param>
        protected DmsObject(Skyline.DataMiner.Library.Common.IDms dms)
        {
            if (dms == null)
            {
                throw new System.ArgumentNullException("dms");
            }

            this.dms = dms;
        }

        /// <summary>
        /// Gets the DataMiner system this object belongs to.
        /// </summary>
        public Skyline.DataMiner.Library.Common.IDms Dms
        {
            get
            {
                return dms;
            }
        }

        /// <summary>
        /// Gets the communication object.
        /// </summary>
        internal Skyline.DataMiner.Library.Common.ICommunication Communication
        {
            get
            {
                return dms.Communication;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether or not the DMS object has been loaded.
        /// </summary>
        internal bool IsLoaded
        {
            get
            {
                return isLoaded;
            }

            set
            {
                isLoaded = value;
            }
        }

        /// <summary>
        /// Loads DMS object data in case the object exists and is not already loaded.
        /// </summary>
        internal void LoadOnDemand()
        {
            if (!IsLoaded)
            {
                Load();
            }
        }

        /// <summary>
        /// Loads the object.
        /// </summary>
        internal abstract void Load();
    }

    /// <summary>
    /// DataMiner object interface.
    /// </summary>
    public interface IDmsObject
    {
    }

    /// <summary>
    /// Represents a DataMiner element.
    /// </summary>
    internal class DmsElement : Skyline.DataMiner.Library.Common.DmsObject, Skyline.DataMiner.Library.Common.IDmsElement
    {
        // Keep this message in case we need to parse the element properties when the user wants to use these.
        private Skyline.DataMiner.Net.Messages.ElementInfoEventMessage elementInfo;
        /// <summary>
        /// The advanced settings.
        /// </summary>
        private Skyline.DataMiner.Library.Common.AdvancedSettings advancedSettings;
        /// <summary>
        /// The device settings.
        /// </summary>
        private Skyline.DataMiner.Library.Common.DeviceSettings deviceSettings;
        /// <summary>
        /// The DVE settings.
        /// </summary>
        private Skyline.DataMiner.Library.Common.DveSettings dveSettings;
        /// <summary>
        /// The failover settings.
        /// </summary>
        private Skyline.DataMiner.Library.Common.FailoverSettings failoverSettings;
        /// <summary>
        /// The general settings.
        /// </summary>
        private Skyline.DataMiner.Library.Common.GeneralSettings generalSettings;
        /// <summary>
        /// The redundancy settings.
        /// </summary>
        private Skyline.DataMiner.Library.Common.RedundancySettings redundancySettings;
        /// <summary>
        /// The replication settings.
        /// </summary>
        private Skyline.DataMiner.Library.Common.ReplicationSettings replicationSettings;
        /// <summary>
        /// The element components.
        /// </summary>
        private System.Collections.Generic.IList<Skyline.DataMiner.Library.Common.ElementSettings> settings;
        /// <summary>
        /// Collection of connections available on the element.
        /// </summary>
        private Skyline.DataMiner.Library.Common.ElementConnectionCollection elementCommunicationConnections;
        /// <summary>
        /// Initializes a new instance of the <see cref = "DmsElement"/> class.
        /// </summary>
        /// <param name = "dms">Object implementing <see cref = "IDms"/> interface.</param>
        /// <param name = "dmsElementId">The system-wide element ID.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "dms"/> is <see langword = "null"/>.</exception>
        internal DmsElement(Skyline.DataMiner.Library.Common.IDms dms, Skyline.DataMiner.Library.Common.DmsElementId dmsElementId): base(dms)
        {
            Initialize();
            generalSettings.DmsElementId = dmsElementId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref = "DmsElement"/> class.
        /// </summary>
        /// <param name = "dms">Object implementing the <see cref = "IDms"/> interface.</param>
        /// <param name = "elementInfo">The element information.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "dms"/> is <see langword = "null"/>.</exception>
        /// <exception cref = "ArgumentNullException"><paramref name = "elementInfo"/> is <see langword = "null"/>.</exception>
        internal DmsElement(Skyline.DataMiner.Library.Common.IDms dms, Skyline.DataMiner.Net.Messages.ElementInfoEventMessage elementInfo): base(dms)
        {
            if (elementInfo == null)
            {
                throw new System.ArgumentNullException("elementInfo");
            }

            Initialize(elementInfo);
            Parse(elementInfo);
        }

        /// <summary>
        /// Gets or sets the element description.
        /// </summary>
        /// <value>The element description.</value>
        public string Description
        {
            get
            {
                return GeneralSettings.Description;
            }

            set
            {
                GeneralSettings.Description = value;
            }
        }

        /// <summary>
        /// Gets the system-wide element ID of the element.
        /// </summary>
        /// <value>The system-wide element ID of the element.</value>
        public Skyline.DataMiner.Library.Common.DmsElementId DmsElementId
        {
            get
            {
                return generalSettings.DmsElementId;
            }
        }

        /// <summary>
        /// Gets the DVE settings of this element.
        /// </summary>
        /// <value>The DVE settings of this element.</value>
        public Skyline.DataMiner.Library.Common.IDveSettings DveSettings
        {
            get
            {
                return dveSettings;
            }
        }

        /// <summary>
        /// Gets the DataMiner Agent that hosts this element.
        /// </summary>
        /// <value>The DataMiner Agent that hosts this element.</value>
        public Skyline.DataMiner.Library.Common.IDma Host
        {
            get
            {
                return generalSettings.Host;
            }
        }

        /// <summary>
        /// Gets or sets the element name.
        /// </summary>
        /// <value>The element name.</value>
        /// <exception cref = "ArgumentNullException">The value of a set operation is <see langword = "null"/>.</exception>
        /// <exception cref = "ArgumentException">The value of a set operation is empty or white space.</exception>
        /// <exception cref = "ArgumentException">The value of a set operation exceeds 200 characters.</exception>
        /// <exception cref = "ArgumentException">The value of a set operation contains a forbidden character.</exception>
        /// <exception cref = "ArgumentException">The value of a set operation contains more than one '%' character.</exception>
        /// <exception cref = "NotSupportedException">A set operation is not supported on a DVE child or a derived element.</exception>
        /// <remarks>
        /// <para>The following restrictions apply to element names:</para>
        /// <list type = "bullet">
        ///		<item><para>Names may not start or end with the following characters: '.' (dot), ' ' (space).</para></item>
        ///		<item><para>Names may not contain the following characters: '\', '/', ':', '*', '?', '"', '&lt;', '&gt;', '|', '°', ';'.</para></item>
        ///		<item><para>The following characters may not occur more than once within a name: '%' (percentage).</para></item>
        /// </list>
        /// </remarks>
        public string Name
        {
            get
            {
                return generalSettings.Name;
            }

            set
            {
                generalSettings.Name = Skyline.DataMiner.Library.Common.InputValidator.ValidateName(value, "value");
            }
        }

        /// <summary>
        /// Gets the protocol executed by this element.
        /// </summary>
        /// <value>The protocol executed by this element.</value>
        public Skyline.DataMiner.Library.Common.IDmsProtocol Protocol
        {
            get
            {
                return generalSettings.Protocol;
            }
        }

        /// <summary>
        /// Gets the redundancy settings.
        /// </summary>
        /// <value>The redundancy settings.</value>
        public Skyline.DataMiner.Library.Common.IRedundancySettings RedundancySettings
        {
            get
            {
                return redundancySettings;
            }
        }

        /// <summary>
        /// Gets the element state.
        /// </summary>
        /// <value>The element state.</value>
        public Skyline.DataMiner.Library.Common.ElementState State
        {
            get
            {
                return GeneralSettings.State;
            }

            internal set
            {
                GeneralSettings.State = value;
            }
        }

        /// <summary>
        /// Gets the general settings of the element.
        /// </summary>
        internal Skyline.DataMiner.Library.Common.GeneralSettings GeneralSettings
        {
            get
            {
                return generalSettings;
            }
        }

        /// <summary>
        /// Gets the specified standalone parameter.
        /// </summary>
        /// <typeparam name = "T">The type of the parameter. Currently supported types: int?, double?, DateTime? and string.</typeparam>
        /// <param name = "parameterId">The parameter ID.</param>
        /// <exception cref = "ArgumentException"><paramref name = "parameterId"/> is invalid.</exception>
        /// <exception cref = "ElementNotFoundException">The element was not found in the DataMiner System.</exception>
        /// <exception cref = "ElementStoppedException">The element is stopped.</exception>
        /// <exception cref = "NotSupportedException">A type other than string, int?, double? or DateTime? was provided.</exception>
        /// <returns>The standalone parameter that corresponds with the specified ID.</returns>
        public Skyline.DataMiner.Library.Common.IDmsStandaloneParameter<T> GetStandaloneParameter<T>(int parameterId)
        {
            if (parameterId < 1)
            {
                throw new System.ArgumentException("Invalid parameter ID.", "parameterId");
            }

            System.Type type = typeof(T);
            if (type != typeof(string) && type != typeof(int? ) && type != typeof(double? ) && type != typeof(System.DateTime? ))
            {
                throw new System.NotSupportedException("Only one of the following types is supported: string, int?, double? or DateTime?.");
            }

            Skyline.DataMiner.Library.Common.HelperClass.CheckElementState(this);
            return new Skyline.DataMiner.Library.Common.DmsStandaloneParameter<T>(this, parameterId);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendFormat(System.Globalization.CultureInfo.InvariantCulture, "Name: {0}{1}", Name, System.Environment.NewLine);
            sb.AppendFormat(System.Globalization.CultureInfo.InvariantCulture, "agent ID/element ID: {0}{1}", DmsElementId.Value, System.Environment.NewLine);
            sb.AppendFormat(System.Globalization.CultureInfo.InvariantCulture, "Description: {0}{1}", Description, System.Environment.NewLine);
            sb.AppendFormat(System.Globalization.CultureInfo.InvariantCulture, "Protocol name: {0}{1}", Protocol.Name, System.Environment.NewLine);
            sb.AppendFormat(System.Globalization.CultureInfo.InvariantCulture, "Protocol version: {0}{1}", Protocol.Version, System.Environment.NewLine);
            sb.AppendFormat(System.Globalization.CultureInfo.InvariantCulture, "Hosting agent ID: {0}{1}", Host.Id, System.Environment.NewLine);
            return sb.ToString();
        }

        /// <summary>
        /// Loads all the data and properties found related to the element.
        /// </summary>
        /// <exception cref = "ElementNotFoundException">The element was not found in the DataMiner system.</exception>
        internal override void Load()
        {
            try
            {
                IsLoaded = true;
                Skyline.DataMiner.Net.Messages.GetElementByIDMessage message = new Skyline.DataMiner.Net.Messages.GetElementByIDMessage(generalSettings.DmsElementId.AgentId, generalSettings.DmsElementId.ElementId);
                Skyline.DataMiner.Net.Messages.ElementInfoEventMessage response = (Skyline.DataMiner.Net.Messages.ElementInfoEventMessage)Communication.SendSingleResponseMessage(message);
                elementCommunicationConnections = new Skyline.DataMiner.Library.Common.ElementConnectionCollection(response);
                Parse(response);
            }
            catch (Skyline.DataMiner.Net.Exceptions.DataMinerException e)
            {
                if (e.ErrorCode == -2146233088)
                {
                    // 0x80131500, Element "[element ID]" is unavailable.
                    throw new Skyline.DataMiner.Library.Common.ElementNotFoundException(DmsElementId, e);
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Parses all of the element info.
        /// </summary>
        /// <param name = "elementInfo">The element info message.</param>
        internal void Parse(Skyline.DataMiner.Net.Messages.ElementInfoEventMessage elementInfo)
        {
            IsLoaded = true;
            try
            {
                ParseElementInfo(elementInfo);
            }
            catch
            {
                IsLoaded = false;
                throw;
            }
        }

        /// <summary>
        /// Initializes the element.
        /// </summary>
        private void Initialize(Skyline.DataMiner.Net.Messages.ElementInfoEventMessage elementInfo)
        {
            this.elementInfo = elementInfo;
            generalSettings = new Skyline.DataMiner.Library.Common.GeneralSettings(this);
            deviceSettings = new Skyline.DataMiner.Library.Common.DeviceSettings(this);
            replicationSettings = new Skyline.DataMiner.Library.Common.ReplicationSettings(this);
            advancedSettings = new Skyline.DataMiner.Library.Common.AdvancedSettings(this);
            failoverSettings = new Skyline.DataMiner.Library.Common.FailoverSettings(this);
            redundancySettings = new Skyline.DataMiner.Library.Common.RedundancySettings(this);
            dveSettings = new Skyline.DataMiner.Library.Common.DveSettings(this);
            elementCommunicationConnections = new Skyline.DataMiner.Library.Common.ElementConnectionCollection(this.elementInfo);
            settings = new System.Collections.Generic.List<Skyline.DataMiner.Library.Common.ElementSettings>{generalSettings, deviceSettings, replicationSettings, advancedSettings, failoverSettings, redundancySettings, dveSettings};
        }

        /// <summary>
        /// Initializes the element.
        /// </summary>
        private void Initialize()
        {
            generalSettings = new Skyline.DataMiner.Library.Common.GeneralSettings(this);
            deviceSettings = new Skyline.DataMiner.Library.Common.DeviceSettings(this);
            replicationSettings = new Skyline.DataMiner.Library.Common.ReplicationSettings(this);
            advancedSettings = new Skyline.DataMiner.Library.Common.AdvancedSettings(this);
            failoverSettings = new Skyline.DataMiner.Library.Common.FailoverSettings(this);
            redundancySettings = new Skyline.DataMiner.Library.Common.RedundancySettings(this);
            dveSettings = new Skyline.DataMiner.Library.Common.DveSettings(this);
            settings = new System.Collections.Generic.List<Skyline.DataMiner.Library.Common.ElementSettings>{generalSettings, deviceSettings, replicationSettings, advancedSettings, failoverSettings, redundancySettings, dveSettings};
        }

        /// <summary>
        /// Parses the element info.
        /// </summary>
        /// <param name = "elementInfo">The element info.</param>
        private void ParseElementInfo(Skyline.DataMiner.Net.Messages.ElementInfoEventMessage elementInfo)
        {
            // Keep this object in case properties are accessed.
            this.elementInfo = elementInfo;
            foreach (Skyline.DataMiner.Library.Common.ElementSettings component in settings)
            {
                component.Load(elementInfo);
            }

            ParseConnections(elementInfo);
        }

        /// <summary>
        /// Parse an ElementInfoEventMessage object.
        /// </summary>
        /// <param name = "elementInfo"></param>
        private void ParseConnections(Skyline.DataMiner.Net.Messages.ElementInfoEventMessage elementInfo)
        {
            // Keep this object in case properties are accessed.
            this.elementInfo = elementInfo;
            ParseConnection(elementInfo.MainPort);
            if (elementInfo.ExtraPorts != null)
            {
                foreach (Skyline.DataMiner.Net.Messages.ElementPortInfo info in elementInfo.ExtraPorts)
                {
                    ParseConnection(info);
                }
            }
        }

        /// <summary>
        /// Parse an ElementPortInfo object in order to add IElementConnection objects to the ElementConnectionCollection.
        /// </summary>
        /// <param name = "info">The ElementPortInfo object.</param>
        private void ParseConnection(Skyline.DataMiner.Net.Messages.ElementPortInfo info)
        {
            switch (info.ProtocolType)
            {
                case Skyline.DataMiner.Net.Messages.ProtocolType.Virtual:
                    Skyline.DataMiner.Library.Common.VirtualConnection myVirtualConnection = new Skyline.DataMiner.Library.Common.VirtualConnection(info);
                    elementCommunicationConnections[info.PortID] = myVirtualConnection;
                    break;
                case Skyline.DataMiner.Net.Messages.ProtocolType.SnmpV1:
                    Skyline.DataMiner.Library.Common.SnmpV1Connection mySnmpV1Connection = new Skyline.DataMiner.Library.Common.SnmpV1Connection(info);
                    elementCommunicationConnections[info.PortID] = mySnmpV1Connection;
                    break;
                case Skyline.DataMiner.Net.Messages.ProtocolType.SnmpV2:
                    Skyline.DataMiner.Library.Common.SnmpV2Connection mySnmpv2Connection = new Skyline.DataMiner.Library.Common.SnmpV2Connection(info);
                    elementCommunicationConnections[info.PortID] = mySnmpv2Connection;
                    break;
                case Skyline.DataMiner.Net.Messages.ProtocolType.SnmpV3:
                    Skyline.DataMiner.Library.Common.SnmpV3Connection mySnmpV3Connection = new Skyline.DataMiner.Library.Common.SnmpV3Connection(info);
                    elementCommunicationConnections[info.PortID] = mySnmpV3Connection;
                    break;
                default:
                    Skyline.DataMiner.Library.Common.RealConnection myConnection = new Skyline.DataMiner.Library.Common.RealConnection(info);
                    elementCommunicationConnections[info.PortID] = myConnection;
                    break;
            }
        }
    }

    /// <summary>
    /// DataMiner element interface.
    /// </summary>
    public interface IDmsElement : Skyline.DataMiner.Library.Common.IDmsObject, Skyline.DataMiner.Library.Common.IUpdateable
    {
        /// <summary>
        /// Gets or sets the element description.
        /// </summary>
        /// <value>The element description.</value>
        string Description
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the system-wide element ID of the element.
        /// </summary>
        /// <value>The system-wide element ID of the element.</value>
        Skyline.DataMiner.Library.Common.DmsElementId DmsElementId
        {
            get;
        }

        /// <summary>
        /// Gets the DVE settings of this element.
        /// </summary>
        /// <value>The DVE settings of this element.</value>
        Skyline.DataMiner.Library.Common.IDveSettings DveSettings
        {
            get;
        }

        ///// <summary>
        ///// Gets the failover settings of this element.
        ///// </summary>
        ///// <value>The failover settings of this element.</value>
        //IFailoverSettings FailoverSettings { get; }
        /// <summary>
        /// Gets the DataMiner Agent that hosts this element.
        /// </summary>
        /// <value>The DataMiner Agent that hosts this element.</value>
        Skyline.DataMiner.Library.Common.IDma Host
        {
            get;
        }

        /// <summary>
        /// Gets or sets the element name.
        /// </summary>
        /// <value>The element name.</value>
        /// <exception cref = "ArgumentNullException">The value of a set operation is <see langword = "null"/>.</exception>
        /// <exception cref = "ArgumentException">The value of a set operation is empty or white space.</exception>
        /// <exception cref = "ArgumentException">The value of a set operation exceeds 200 characters.</exception>
        /// <exception cref = "ArgumentException">The value of a set operation contains a forbidden character.</exception>
        /// <exception cref = "ArgumentException">The value of a set operation contains more than one '%' character.</exception>
        /// <exception cref = "NotSupportedException">A set operation is not supported on a DVE child or a derived element.</exception>
        /// <remarks>
        /// <para>The following restrictions apply to element names:</para>
        /// <list type = "bullet">
        ///		<item><para>Names may not start or end with the following characters: '.' (dot), ' ' (space).</para></item>
        ///		<item><para>Names may not contain the following characters: '\', '/', ':', '*', '?', '"', '&lt;', '&gt;', '|', '°', ';'.</para></item>
        ///		<item><para>The following characters may not occur more than once within a name: '%' (percentage).</para></item>
        /// </list>
        /// </remarks>
        string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the protocol executed by this element.
        /// </summary>
        /// <value>The protocol executed by this element.</value>
        Skyline.DataMiner.Library.Common.IDmsProtocol Protocol
        {
            get;
        }

        /// <summary>
        /// Gets the redundancy settings.
        /// </summary>
        /// <value>The redundancy settings.</value>
        Skyline.DataMiner.Library.Common.IRedundancySettings RedundancySettings
        {
            get;
        }

        /// <summary>
        /// Gets the element state.
        /// </summary>
        /// <value>The element state.</value>
        Skyline.DataMiner.Library.Common.ElementState State
        {
            get;
        }

        /// <summary>
        /// Gets the specified standalone parameter.
        /// </summary>
        /// <typeparam name = "T">The type of the parameter. Currently supported types: int?, double?, DateTime? and string.</typeparam>
        /// <param name = "parameterId">The parameter ID.</param>
        /// <exception cref = "ArgumentException"><paramref name = "parameterId"/> is invalid.</exception>
        /// <exception cref = "ElementNotFoundException">The element was not found in the DataMiner System.</exception>
        /// <exception cref = "ElementStoppedException">The element is not active.</exception>
        /// <exception cref = "NotSupportedException">A type other than string, int?, double? or DateTime? was provided.</exception>
        /// <returns>The standalone parameter that corresponds with the specified ID.</returns>
        Skyline.DataMiner.Library.Common.IDmsStandaloneParameter<T> GetStandaloneParameter<T>(int parameterId);
    }

    /// <summary>
    /// Base class for all connection related objects.
    /// </summary>
    public abstract class ConnectionSettings
    {
        /// <summary>
        /// Enum used to track changes on properties of classes implementing this abstract class
        /// </summary>
        protected enum ConnectionSetting
        {
            /// <summary>
            /// GetCommunityString
            /// </summary>
            GetCommunityString = 0,
            /// <summary>
            /// SetCommunityString
            /// </summary>
            SetCommunityString = 1,
            /// <summary>
            /// DeviceAddress
            /// </summary>
            DeviceAddress = 2,
            /// <summary>
            /// Timeout
            /// </summary>
            Timeout = 3,
            /// <summary>
            /// Retries
            /// </summary>
            Retries = 4,
            /// <summary>
            /// ElementTimeout
            /// </summary>
            ElementTimeout = 5,
            /// <summary>
            /// PortConnection (e.g.Udp , Tcp)
            /// </summary>
            PortConnection = 6,
            /// <summary>
            /// SecurityConfiguration
            /// </summary>
            SecurityConfig = 7,
            /// <summary>
            /// SNMPv3 Encryption Algorithm
            /// </summary>
            EncryptionAlgorithm = 8,
            /// <summary>
            /// SNMPv3 AuthenticationProtocol
            /// </summary>
            AuthenticationProtocol = 9,
            /// <summary>
            /// SNMPv3 EncryptionKey
            /// </summary>
            EncryptionKey = 10,
            /// <summary>
            /// SNMPv3 AuthenticationKey
            /// </summary>
            AuthenticationKey = 11,
            /// <summary>
            /// SNMPv3 Username
            /// </summary>
            Username = 12,
            /// <summary>
            /// SNMPv3 Security Level and Protocol
            /// </summary>
            SecurityLevelAndProtocol = 13,
            /// <summary>
            /// Local port
            /// </summary>
            LocalPort = 14,
            /// <summary>
            /// Remote port
            /// </summary>
            RemotePort = 15,
            /// <summary>
            /// Is SSL/TLS enabled
            /// </summary>
            IsSslTlsEnabled = 16,
            /// <summary>
            /// Remote host
            /// </summary>
            RemoteHost = 17,
            /// <summary>
            /// Network interface card
            /// </summary>
            NetworkInterfaceCard = 18
        }

        /// <summary>
        /// The list of changed properties.
        /// </summary>
        private readonly System.Collections.Generic.List<Skyline.DataMiner.Library.Common.ConnectionSettings.ConnectionSetting> changedPropertyList = new System.Collections.Generic.List<Skyline.DataMiner.Library.Common.ConnectionSettings.ConnectionSetting>();
        /// <summary>
        /// Gets the list of updated properties.
        /// </summary>
        protected System.Collections.Generic.List<Skyline.DataMiner.Library.Common.ConnectionSettings.ConnectionSetting> ChangedPropertyList
        {
            get
            {
                return changedPropertyList;
            }
        }
    }

    /// <summary>
    /// Represents a connection of a DataMiner element.
    /// </summary>
    public interface IElementConnection
    {
    }

    /// <summary>
    /// Defines a non-virtual interface.
    /// </summary>
    public interface IRealConnection : Skyline.DataMiner.Library.Common.IElementConnection
    {
    }

    /// <summary>
    /// Defines an SNMP connection.
    /// </summary>
    public interface ISnmpConnection : Skyline.DataMiner.Library.Common.IRealConnection
    {
    }

    /// <summary>
    /// Defines an SNMPv1 Connection
    /// </summary>
    public interface ISnmpV1Connection : Skyline.DataMiner.Library.Common.ISnmpConnection
    {
    }

    /// <summary>
    /// Defines an SNMPv2 Connection.
    /// </summary>
    public interface ISnmpV2Connection : Skyline.DataMiner.Library.Common.ISnmpConnection
    {
    }

    /// <summary>
    /// Defines an SNMPv3 Connection.
    /// </summary>
    public interface ISnmpV3Connection : Skyline.DataMiner.Library.Common.ISnmpConnection
    {
        /// <summary>
        /// Gets or sets the SNMPv3 security configuration.
        /// </summary>
        Skyline.DataMiner.Library.Common.ISnmpV3SecurityConfig SecurityConfig
        {
            get;
            set;
        }
    }

    /// <summary>
    /// Interface for SnmpV3 Security configurations.
    /// </summary>
    public interface ISnmpV3SecurityConfig
    {
    }

    /// <summary>
    /// Defines a Virtual Connection
    /// </summary>
    public interface IVirtualConnection : Skyline.DataMiner.Library.Common.IElementConnection
    {
    }

    /// <summary>
    /// Class representing any non-virtual connection.
    /// </summary>
    public class RealConnection : Skyline.DataMiner.Library.Common.ConnectionSettings, Skyline.DataMiner.Library.Common.IRealConnection
    {
        private readonly int portId;
        private System.TimeSpan timeout;
        private int retries;
        /// <summary>
        /// Initiates a new RealConnection class.
        /// </summary>
        /// <param name = "info"></param>
        internal RealConnection(Skyline.DataMiner.Net.Messages.ElementPortInfo info)
        {
            this.portId = info.PortID;
            this.retries = info.Retries;
            this.timeout = new System.TimeSpan(0, 0, 0, 0, info.TimeoutTime);
        }
    }

    /// <summary>
    /// Class used to Encrypt data in DataMiner.
    /// </summary>
    internal static class RSA
    {
        private static System.Security.Cryptography.RSAParameters publicKey;
        /// <summary>
        /// Get or Sets the Public Key.
        /// </summary>
        internal static System.Security.Cryptography.RSAParameters PublicKey
        {
            get
            {
                return publicKey;
            }

            set
            {
                publicKey = value;
            }
        }
    }

    /// <summary>
    /// Class representing an SNMPv1 connection.
    /// </summary>
    public class SnmpV1Connection : Skyline.DataMiner.Library.Common.ConnectionSettings, Skyline.DataMiner.Library.Common.ISnmpV1Connection
    {
        private string getCommunityString;
        private string setCommunityString;
        private string deviceAddress;
        private System.TimeSpan timeout;
        private int retries;
        private Skyline.DataMiner.Library.Common.IUdp udpIpConfiguration;
        private readonly int id;
        private readonly System.Guid libraryCredentials;
        private System.TimeSpan? elementTimeout;
        /// <summary>
        /// Initiates an new instance.
        /// </summary>
        internal SnmpV1Connection(Skyline.DataMiner.Net.Messages.ElementPortInfo info)
        {
            this.deviceAddress = info.BusAddress;
            this.retries = info.Retries;
            this.timeout = new System.TimeSpan(0, 0, 0, 0, info.TimeoutTime);
            this.libraryCredentials = info.LibraryCredential;
            //this.elementTimeout = new TimeSpan(0, 0, info.ElementTimeoutTime / 1000);
            if (this.libraryCredentials == System.Guid.Empty)
            {
                this.getCommunityString = info.GetCommunity;
                this.setCommunityString = info.SetCommunity;
            }
            else
            {
                this.getCommunityString = System.String.Empty;
                this.setCommunityString = System.String.Empty;
            }

            this.id = info.PortID;
            this.elementTimeout = new System.TimeSpan(0, 0, 0, 0, info.ElementTimeoutTime);
            this.udpIpConfiguration = new Skyline.DataMiner.Library.Common.Udp(info);
        }

        /// <summary>
        /// /// Initiates a new instance with default settings for Get Community String (public), Set Community String (private), Device Address (empty),
        /// Command Timeout (1500ms), Retries (3) and Element Timeout (30s).
        /// </summary>
        /// <param name = "udpConfiguration">The UDP configuration parameters.</param>
        public SnmpV1Connection(Skyline.DataMiner.Library.Common.IUdp udpConfiguration)
        {
            if (udpConfiguration == null)
            {
                throw new System.ArgumentNullException("udpConfiguration");
            }

            this.id = -1;
            this.udpIpConfiguration = udpConfiguration;
            this.getCommunityString = "public";
            this.setCommunityString = "private";
            this.deviceAddress = System.String.Empty;
            this.timeout = new System.TimeSpan(0, 0, 0, 0, 1500);
            this.retries = 3;
            this.elementTimeout = new System.TimeSpan(0, 0, 0, 30);
        }
    }

    /// <summary>
    /// Class representing an SnmpV2 Connection.
    /// </summary>
    public class SnmpV2Connection : Skyline.DataMiner.Library.Common.ConnectionSettings, Skyline.DataMiner.Library.Common.ISnmpV2Connection
    {
        private System.TimeSpan timeout;
        private int retries;
        private string deviceAddress;
        private Skyline.DataMiner.Library.Common.IUdp udpIpConfiguration;
        private string getCommunityString;
        private string setCommunityString;
        private readonly int portId;
        private readonly System.Guid libraryCredentials;
        private System.TimeSpan? elementTimeout;
        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        internal SnmpV2Connection(Skyline.DataMiner.Net.Messages.ElementPortInfo info)
        {
            this.deviceAddress = info.BusAddress;
            this.retries = info.Retries;
            this.timeout = new System.TimeSpan(0, 0, 0, 0, info.TimeoutTime);
            this.getCommunityString = info.GetCommunity;
            this.setCommunityString = info.SetCommunity;
            this.libraryCredentials = info.LibraryCredential;
            if (info.LibraryCredential == System.Guid.Empty)
            {
                this.getCommunityString = info.GetCommunity;
                this.setCommunityString = info.SetCommunity;
            }
            else
            {
                this.getCommunityString = System.String.Empty;
                this.setCommunityString = System.String.Empty;
            }

            this.portId = info.PortID;
            this.elementTimeout = new System.TimeSpan(0, 0, 0, 0, info.ElementTimeoutTime);
            this.udpIpConfiguration = new Skyline.DataMiner.Library.Common.Udp(info);
        }

        /// <summary>
        ///	Initiates a new instance with default settings for Get Community String (public), Set Community String (private), Device Address (empty),
        /// Command Timeout (1500ms), Retries (3) and Element Timeout (30s).
        /// </summary>
        /// <param name = "udpConfiguration">The UDP Connection settings.</param>
        public SnmpV2Connection(Skyline.DataMiner.Library.Common.IUdp udpConfiguration)
        {
            if (udpConfiguration == null)
            {
                throw new System.ArgumentNullException("udpConfiguration");
            }

            this.portId = -1;
            this.udpIpConfiguration = udpConfiguration;
            //this.udpIpConfiguration = udpIpIpConfiguration;
            this.deviceAddress = System.String.Empty;
            this.getCommunityString = "public";
            this.setCommunityString = "private";
            this.timeout = new System.TimeSpan(0, 0, 0, 0, 1500);
            this.retries = 3;
            this.elementTimeout = new System.TimeSpan(0, 0, 0, 30);
            this.libraryCredentials = System.Guid.Empty;
        }
    }

    /// <summary>
    /// Class representing a SNMPv3 class.
    /// </summary>
    public class SnmpV3Connection : Skyline.DataMiner.Library.Common.ConnectionSettings, Skyline.DataMiner.Library.Common.ISnmpV3Connection
    {
        private System.TimeSpan? elementTimeout;
        private System.TimeSpan timeout;
        private int retries;
        private string deviceAddress;
        private Skyline.DataMiner.Library.Common.IUdp udpIpConfiguration;
        private Skyline.DataMiner.Library.Common.ISnmpV3SecurityConfig securityConfig;
        private readonly int id;
        private readonly System.Guid libraryCredentials;
        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        internal SnmpV3Connection(Skyline.DataMiner.Net.Messages.ElementPortInfo info)
        {
            this.deviceAddress = info.BusAddress;
            this.retries = info.Retries;
            this.timeout = new System.TimeSpan(0, 0, 0, 0, info.TimeoutTime);
            this.elementTimeout = new System.TimeSpan(0, 0, info.ElementTimeoutTime / 1000);
            if (this.libraryCredentials == System.Guid.Empty)
            {
                Skyline.DataMiner.Library.Common.SnmpV3SecurityLevelAndProtocol securityLevelAndProtocol = Skyline.DataMiner.Library.Common.HelperClass.GetEnumFromDescription<Skyline.DataMiner.Library.Common.SnmpV3SecurityLevelAndProtocol>(info.StopBits);
                Skyline.DataMiner.Library.Common.SnmpV3EncryptionAlgorithm encryptionAlgorithm = Skyline.DataMiner.Library.Common.HelperClass.GetEnumFromDescription<Skyline.DataMiner.Library.Common.SnmpV3EncryptionAlgorithm>(info.FlowControl);
                Skyline.DataMiner.Library.Common.SnmpV3AuthenticationAlgorithm authenticationProtocol = Skyline.DataMiner.Library.Common.HelperClass.GetEnumFromDescription<Skyline.DataMiner.Library.Common.SnmpV3AuthenticationAlgorithm>(info.Parity);
                string authenticationKey = info.GetCommunity;
                string encryptionKey = info.SetCommunity;
                string username = info.DataBits;
                this.securityConfig = new Skyline.DataMiner.Library.Common.SnmpV3SecurityConfig(securityLevelAndProtocol, username, authenticationKey, encryptionKey, authenticationProtocol, encryptionAlgorithm);
            }
            else
            {
                this.SecurityConfig = new Skyline.DataMiner.Library.Common.SnmpV3SecurityConfig(Skyline.DataMiner.Library.Common.SnmpV3SecurityLevelAndProtocol.DefinedInCredentialsLibrary, "", "", "", Skyline.DataMiner.Library.Common.SnmpV3AuthenticationAlgorithm.DefinedInCredentialsLibrary, Skyline.DataMiner.Library.Common.SnmpV3EncryptionAlgorithm.DefinedInCredentialsLibrary);
            }

            this.id = info.PortID;
            this.elementTimeout = new System.TimeSpan(0, 0, 0, 0, info.ElementTimeoutTime);
            this.udpIpConfiguration = new Skyline.DataMiner.Library.Common.Udp(info);
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name = "udpConfiguration">The udp configuration settings.</param>
        ///<param name = "securityConfig">The SNMPv3 security configuration.</param>
        public SnmpV3Connection(Skyline.DataMiner.Library.Common.IUdp udpConfiguration, Skyline.DataMiner.Library.Common.SnmpV3SecurityConfig securityConfig)
        {
            if (udpConfiguration == null)
            {
                throw new System.ArgumentNullException("udpConfiguration");
            }

            if (securityConfig == null)
            {
                throw new System.ArgumentNullException("securityConfig");
            }

            this.libraryCredentials = System.Guid.Empty;
            this.id = -1;
            this.udpIpConfiguration = udpConfiguration;
            this.deviceAddress = System.String.Empty;
            this.securityConfig = securityConfig;
            this.timeout = new System.TimeSpan(0, 0, 0, 0, 1500);
            this.retries = 3;
            this.elementTimeout = new System.TimeSpan(0, 0, 0, 30);
        }

        /// <summary>
        /// Gets or sets the SNMPv3 security configuration.
        /// </summary>
        public Skyline.DataMiner.Library.Common.ISnmpV3SecurityConfig SecurityConfig
        {
            get
            {
                return securityConfig;
            }

            set
            {
                ChangedPropertyList.Add(Skyline.DataMiner.Library.Common.ConnectionSettings.ConnectionSetting.SecurityConfig);
                securityConfig = value;
            }
        }
    }

    /// <summary>
    /// Represents the Security settings linked to SNMPv3.
    /// </summary>
    public class SnmpV3SecurityConfig : Skyline.DataMiner.Library.Common.ConnectionSettings, Skyline.DataMiner.Library.Common.ISnmpV3SecurityConfig
    {
        private Skyline.DataMiner.Library.Common.SnmpV3SecurityLevelAndProtocol securityLevelAndProtocol;
        private string username;
        private string authenticationKey;
        private string encryptionKey;
        private Skyline.DataMiner.Library.Common.SnmpV3AuthenticationAlgorithm authenticationAlgorithm;
        private Skyline.DataMiner.Library.Common.SnmpV3EncryptionAlgorithm encryptionAlgorithm;
        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name = "securityLevelAndProtocol">The security Level and Protocol.</param>
        /// <param name = "username">The username.</param>
        /// <param name = "authenticationKey">The authenticationKey</param>
        /// <param name = "encryptionKey">The encryptionKey</param>
        /// <param name = "authenticationAlgorithm">The authentication Algorithm.</param>
        /// <param name = "encryptionAlgorithm">The encryption Algorithm.</param>
        /// <exception cref = "System.ArgumentNullException">When username, authenticationKey or encryptionKey is null.</exception>
        internal SnmpV3SecurityConfig(Skyline.DataMiner.Library.Common.SnmpV3SecurityLevelAndProtocol securityLevelAndProtocol, string username, string authenticationKey, string encryptionKey, Skyline.DataMiner.Library.Common.SnmpV3AuthenticationAlgorithm authenticationAlgorithm, Skyline.DataMiner.Library.Common.SnmpV3EncryptionAlgorithm encryptionAlgorithm)
        {
            if (username == null)
            {
                throw new System.ArgumentNullException("username");
            }

            if (authenticationKey == null)
            {
                throw new System.ArgumentNullException("authenticationKey");
            }

            if (encryptionKey == null)
            {
                throw new System.ArgumentNullException("encryptionKey");
            }

            this.securityLevelAndProtocol = securityLevelAndProtocol;
            this.username = username;
            this.authenticationKey = authenticationKey;
            this.encryptionKey = encryptionKey;
            this.authenticationAlgorithm = authenticationAlgorithm;
            this.encryptionAlgorithm = encryptionAlgorithm;
        }

        /// <summary>
        /// Initializes a new instance using No Authentication and No Privacy.
        /// </summary>
        /// <param name = "username">The username.</param>
        /// <exception cref = "System.ArgumentNullException">When the username is null.</exception>
        public SnmpV3SecurityConfig(string username)
        {
            if (username == null)
            {
                throw new System.ArgumentNullException("username");
            }

            this.securityLevelAndProtocol = Skyline.DataMiner.Library.Common.SnmpV3SecurityLevelAndProtocol.NoAuthenticationNoPrivacy;
            this.username = username;
            this.authenticationKey = "";
            this.encryptionKey = "";
            this.authenticationAlgorithm = Skyline.DataMiner.Library.Common.SnmpV3AuthenticationAlgorithm.None;
            this.encryptionAlgorithm = Skyline.DataMiner.Library.Common.SnmpV3EncryptionAlgorithm.None;
        }

        /// <summary>
        /// Initializes a new instance using Authentication No Privacy.
        /// </summary>
        /// <param name = "username">The username.</param>
        /// <param name = "authenticationKey">The Authentication key.</param>
        /// <param name = "authenticationAlgorithm">The Authentication Algorithm.</param>
        /// <exception cref = "System.ArgumentNullException">When username, authenticationKey is null.</exception>
        /// <exception cref = "IncorrectDataException">When None or DefinedInCredentialsLibrary is selected as authentication algorithm.</exception>
        public SnmpV3SecurityConfig(string username, string authenticationKey, Skyline.DataMiner.Library.Common.SnmpV3AuthenticationAlgorithm authenticationAlgorithm)
        {
            if (username == null)
            {
                throw new System.ArgumentNullException("username");
            }

            if (authenticationKey == null)
            {
                throw new System.ArgumentNullException("authenticationKey");
            }

            if ((authenticationAlgorithm == Skyline.DataMiner.Library.Common.SnmpV3AuthenticationAlgorithm.None) || (authenticationAlgorithm == Skyline.DataMiner.Library.Common.SnmpV3AuthenticationAlgorithm.DefinedInCredentialsLibrary))
            {
                throw new Skyline.DataMiner.Library.Common.IncorrectDataException("Authentication Algorithm 'None' and 'DefinedInCredentialsLibrary' is Invalid when choosing 'Authentication No Privacy' as Security Level and Protocol.");
            }

            this.securityLevelAndProtocol = Skyline.DataMiner.Library.Common.SnmpV3SecurityLevelAndProtocol.AuthenticationNoPrivacy;
            this.username = username;
            this.authenticationKey = authenticationKey;
            this.encryptionKey = "";
            this.authenticationAlgorithm = authenticationAlgorithm;
            this.encryptionAlgorithm = Skyline.DataMiner.Library.Common.SnmpV3EncryptionAlgorithm.None;
        }

        /// <summary>
        /// Initializes a new instance using Authentication and Privacy.
        /// </summary>
        /// <param name = "username">The username.</param>
        /// <param name = "authenticationKey">The authentication key.</param>
        /// <param name = "encryptionKey">The encryptionKey.</param>
        /// <param name = "authenticationProtocol">The authentication algorithm.</param>
        /// <param name = "encryptionAlgorithm">The encryption algorithm.</param>
        /// <exception cref = "System.ArgumentNullException">When username, authenticationKey or encryptionKey is null.</exception>
        /// <exception cref = "IncorrectDataException">When None or DefinedInCredentialsLibrary is selected as authentication algorithm or encryption algorithm.</exception>
        public SnmpV3SecurityConfig(string username, string authenticationKey, Skyline.DataMiner.Library.Common.SnmpV3AuthenticationAlgorithm authenticationProtocol, string encryptionKey, Skyline.DataMiner.Library.Common.SnmpV3EncryptionAlgorithm encryptionAlgorithm)
        {
            if (username == null)
            {
                throw new System.ArgumentNullException("username");
            }

            if (authenticationKey == null)
            {
                throw new System.ArgumentNullException("authenticationKey");
            }

            if (encryptionKey == null)
            {
                throw new System.ArgumentNullException("encryptionKey");
            }

            if ((authenticationProtocol == Skyline.DataMiner.Library.Common.SnmpV3AuthenticationAlgorithm.None) || (authenticationProtocol == Skyline.DataMiner.Library.Common.SnmpV3AuthenticationAlgorithm.DefinedInCredentialsLibrary))
            {
                throw new Skyline.DataMiner.Library.Common.IncorrectDataException("Authentication Algorithm 'None' and 'DefinedInCredentialsLibrary' is Invalid when choosing 'Authentication No Privacy' as Security Level and Protocol.");
            }

            if ((encryptionAlgorithm == Skyline.DataMiner.Library.Common.SnmpV3EncryptionAlgorithm.None) || (encryptionAlgorithm == Skyline.DataMiner.Library.Common.SnmpV3EncryptionAlgorithm.DefinedInCredentialsLibrary))
            {
                throw new Skyline.DataMiner.Library.Common.IncorrectDataException("Encryption Algorithm 'None' and 'DefinedInCredentialsLibrary' is Invalid when choosing 'Authentication and Privacy' as Security Level and Protocol.");
            }

            this.securityLevelAndProtocol = Skyline.DataMiner.Library.Common.SnmpV3SecurityLevelAndProtocol.AuthenticationPrivacy;
            this.username = username;
            this.authenticationKey = authenticationKey;
            this.encryptionKey = encryptionKey;
            this.authenticationAlgorithm = authenticationProtocol;
            this.encryptionAlgorithm = encryptionAlgorithm;
        }

        /// <summary>
        /// Interprets the returned Authentication Algorithm from a DMA to be compatible.
        /// https://intranet.skyline.be/DataMiner/Lists/Release%20Notes/DispForm.aspx?ID=24423
        /// </summary>
        /// <param name = "authenticationAlgorithm">the Authentication Algorithm of which its compatible value needs to be retrieved.</param>
        /// <returns>An <see cref = "SnmpV3AuthenticationAlgorithm"/> object.</returns>
        internal static Skyline.DataMiner.Library.Common.SnmpV3AuthenticationAlgorithm GetCompatibleAuthenticationAlgorithmFromDMA(string authenticationAlgorithm)
        {
            if (authenticationAlgorithm.Equals("MD5"))
                return Skyline.DataMiner.Library.Common.SnmpV3AuthenticationAlgorithm.Md5;
            if (authenticationAlgorithm.Equals("SHA"))
                return Skyline.DataMiner.Library.Common.SnmpV3AuthenticationAlgorithm.Sha128;
            throw new System.ArgumentException(authenticationAlgorithm + "Not found in compatibility list.");
        }

        /// <summary>
        /// Indicates whether the value is linked with a breaking change in the software and we have to keep it compatible.
        /// </summary>
        /// <param name = "input">The value to be verified.</param>
        /// <returns>True if a compatibility routine needs to be executed.</returns>
        internal static bool IsMarkedAsCompatibilityChange(string input)
        {
            if (input.Equals("MD5") || input.Equals("SHA"))
                return true;
            return false;
        }
    }

    /// <summary>
    /// Class representing a Virtual connection. 
    /// </summary>
    public class VirtualConnection : Skyline.DataMiner.Library.Common.ConnectionSettings, Skyline.DataMiner.Library.Common.IVirtualConnection
    {
        private readonly int id;
        /// <summary>
        /// Initiates a new VirtualConnection class.
        /// </summary>
        /// <param name = "info"></param>
        internal VirtualConnection(Skyline.DataMiner.Net.Messages.ElementPortInfo info)
        {
            this.id = info.PortID;
        }

        /// <summary>
        /// Initiates a new VirtualConnection class.
        /// </summary>
        public VirtualConnection()
        {
            this.id = -1;
        }
    }

    /// <summary>
    /// Specifies the SNMPv3 authentication protocol.
    /// </summary>
    public enum SnmpV3AuthenticationAlgorithm
    {
        /// <summary>
        /// Message Digest 5 (MD5).
        /// </summary>
        [System.ComponentModel.Description("HMAC-MD5")]
        Md5 = 0,
        /// <summary>
        /// Secure Hash Algorithm (SHA).
        /// </summary>
        [System.ComponentModel.Description("HMAC-SHA")]
        Sha128 = 1,
        /// <summary>
        /// Algorithm is defined in the Credential Library.
        /// </summary>
        DefinedInCredentialsLibrary = 2,
        /// <summary>
        /// No algorithm selected.
        /// </summary>
        [System.ComponentModel.Description("None")]
        None = 3,
    }

    /// <summary>
    /// Specifies the SNMPv3 encryption algorithm.
    /// </summary>
    public enum SnmpV3EncryptionAlgorithm
    {
        /// <summary>
        /// Data Encryption Standard (DES).
        /// </summary>
        [System.ComponentModel.Description("DES")]
        Des = 0,
        /// <summary>
        /// Advanced Encryption Standard (AES) 128 bit.
        /// </summary>
        [System.ComponentModel.Description("AES128")]
        Aes128 = 1,
        /// <summary>
        /// Advanced Encryption Standard is defined in the Credential Library.
        /// </summary>
        DefinedInCredentialsLibrary = 2,
        /// <summary>
        /// No algorithm selected.
        /// </summary>
        [System.ComponentModel.Description("None")]
        None = 3,
    }

    /// <summary>
    /// Specifies the SNMP v3 security level and protocol.
    /// </summary>
    public enum SnmpV3SecurityLevelAndProtocol
    {
        /// <summary>
        /// Authentication and privacy.
        /// </summary>
        [System.ComponentModel.Description("authPriv")]
        AuthenticationPrivacy = 0,
        /// <summary>
        /// Authentication but no privacy.
        /// </summary>
        [System.ComponentModel.Description("authNoPriv")]
        AuthenticationNoPrivacy = 1,
        /// <summary>
        /// No authentication and no privacy.
        /// </summary>
        [System.ComponentModel.Description("noAuthNoPriv")]
        NoAuthenticationNoPrivacy = 2,
        /// <summary>
        /// Security Level and Protocol is defined in the Credential library.
        /// </summary>
        DefinedInCredentialsLibrary = 3
    }

    /// <summary>
    /// Represents a connection using the Internet Protocol (IP).
    /// </summary>
    public interface IIpBased : Skyline.DataMiner.Library.Common.IPortConnection
    {
    }

    /// <summary>
    /// interface IPortConnection for which all connections will inherit from.
    /// </summary>
    public interface IPortConnection
    {
    }

    /// <summary>
    /// Represents a UDP/IP connection.
    /// </summary>
    public interface IUdp : Skyline.DataMiner.Library.Common.IIpBased
    {
    }

    /// <summary>
    /// Class representing an UDP connection.
    /// </summary>
    public class Udp : Skyline.DataMiner.Library.Common.ConnectionSettings, Skyline.DataMiner.Library.Common.IUdp
    {
        private int? localPort;
        private int remotePort;
        private bool isSslTlsEnabled;
        private readonly bool isDedicated;
        private string remoteHost;
        private int networkInterfaceCard;
        /// <summary>
        /// Initializes a new instance, using default values for localPort (null=Auto) SslTlsEnabled (false), IsDedicated (false) and NetworkInterfaceCard (0=Auto)
        /// </summary>
        /// <param name = "remoteHost">The IP or name of the remote host.</param>
        /// <param name = "remotePort">The port number of the remote host.</param>
        public Udp(string remoteHost, int remotePort)
        {
            this.localPort = null;
            this.remotePort = remotePort;
            this.isSslTlsEnabled = false;
            this.isDedicated = false;
            this.remoteHost = remoteHost;
            this.networkInterfaceCard = 0;
        }

        /// <summary>
        /// Initializes a new instance using a <see cref = "ElementPortInfo"/> object.
        /// </summary>
        /// <param name = "info"></param>
        internal Udp(Skyline.DataMiner.Net.Messages.ElementPortInfo info)
        {
            if (!info.LocalIPPort.Equals(""))
                localPort = System.Convert.ToInt32(info.LocalIPPort);
            remoteHost = info.PollingIPAddress;
            remotePort = System.Convert.ToInt32(info.PollingIPPort);
            isDedicated = IsDedicatedConnection(info);
            int networkInterfaceId = System.String.IsNullOrWhiteSpace(info.Number) ? 0 : System.Convert.ToInt32(info.Number);
            networkInterfaceCard = networkInterfaceId;
        }

        /// <summary>
        /// Determines if a connection is using a dedicated connection or not (e.g serial single, smart serial single).
        /// </summary>
        /// <param name = "info">ElementPortInfo</param>
        /// <returns>Whether a connection is marked as single or not.</returns>
        private static bool IsDedicatedConnection(Skyline.DataMiner.Net.Messages.ElementPortInfo info)
        {
            bool isDedicatedConnection = false;
            switch (info.ProtocolType)
            {
                case Skyline.DataMiner.Net.Messages.ProtocolType.SerialSingle:
                case Skyline.DataMiner.Net.Messages.ProtocolType.SmartSerialRawSingle:
                case Skyline.DataMiner.Net.Messages.ProtocolType.SmartSerialSingle:
                    isDedicatedConnection = true;
                    break;
                default:
                    isDedicatedConnection = false;
                    break;
            }

            return isDedicatedConnection;
        }
    }

    /// <summary>
    /// Represents the advanced element information.
    /// </summary>
    internal class AdvancedSettings : Skyline.DataMiner.Library.Common.ElementSettings, Skyline.DataMiner.Library.Common.IAdvancedSettings
    {
        /// <summary>
        /// Value indicating whether the element is hidden.
        /// </summary>
        private bool isHidden;
        /// <summary>
        /// Value indicating whether the element is read-only.
        /// </summary>
        private bool isReadOnly;
        /// <summary>
        /// Indicates whether this is a simulated element.
        /// </summary>
        private bool isSimulation;
        /// <summary>
        /// The element timeout value.
        /// </summary>
        private System.TimeSpan timeout = new System.TimeSpan(0, 0, 30);
        /// <summary>
        /// Initializes a new instance of the <see cref = "AdvancedSettings"/> class.
        /// </summary>
        /// <param name = "dmsElement">The reference to the <see cref = "DmsElement"/> instance this object is part of.</param>
        internal AdvancedSettings(Skyline.DataMiner.Library.Common.DmsElement dmsElement): base(dmsElement)
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether the element is hidden.
        /// </summary>
        /// <value><c>true</c> if the element is hidden; otherwise, <c>false</c>.</value>
        /// <exception cref = "NotSupportedException">A set operation is not supported on a derived element.</exception>
        public bool IsHidden
        {
            get
            {
                DmsElement.LoadOnDemand();
                return isHidden;
            }

            set
            {
                DmsElement.LoadOnDemand();
                if (DmsElement.RedundancySettings.IsDerived)
                {
                    throw new System.NotSupportedException("This operation is not supported on a derived element.");
                }

                if (isHidden != value)
                {
                    ChangedPropertyList.Add("IsHidden");
                    isHidden = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the element is read-only.
        /// </summary>
        /// <value><c>true</c> if the element is read-only; otherwise, <c>false</c>.</value>
        /// <exception cref = "NotSupportedException">A set operation is not supported on a DVE or derived element.</exception>
        public bool IsReadOnly
        {
            get
            {
                DmsElement.LoadOnDemand();
                return isReadOnly;
            }

            set
            {
                if (DmsElement.DveSettings.IsChild || DmsElement.RedundancySettings.IsDerived)
                {
                    throw new System.NotSupportedException("This operation is not supported on a DVE child or derived element.");
                }

                DmsElement.LoadOnDemand();
                if (isReadOnly != value)
                {
                    ChangedPropertyList.Add("IsReadOnly");
                    isReadOnly = value;
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether the element is running a simulation.
        /// </summary>
        /// <value><c>true</c> if the element is running a simulation; otherwise, <c>false</c>.</value>
        public bool IsSimulation
        {
            get
            {
                DmsElement.LoadOnDemand();
                return isSimulation;
            }
        }

        /// <summary>
        /// Gets or sets the element timeout value.
        /// </summary>
        /// <value>The timeout value.</value>
        /// <exception cref = "ArgumentOutOfRangeException">The value specified for a set operation is not in the range of [0,120] s.</exception>
        /// <exception cref = "NotSupportedException">A set operation is not supported on a DVE or derived element.</exception>
        /// <remarks>Fractional seconds are ignored. For example, setting the timeout to a value of 3.5s results in setting it to 3s.</remarks>
        public System.TimeSpan Timeout
        {
            get
            {
                DmsElement.LoadOnDemand();
                return timeout;
            }

            set
            {
                if (DmsElement.DveSettings.IsChild || DmsElement.RedundancySettings.IsDerived)
                {
                    throw new System.NotSupportedException("Setting the timeout is not supported on a DVE child or derived element.");
                }

                DmsElement.LoadOnDemand();
                int timeoutInSeconds = (int)value.TotalSeconds;
                if (timeoutInSeconds < 0 || timeoutInSeconds > 120)
                {
                    throw new System.ArgumentOutOfRangeException("value", "The timeout value must be in the range of [0,120] s.");
                }

                if ((int)timeout.TotalSeconds != (int)value.TotalSeconds)
                {
                    ChangedPropertyList.Add("Timeout");
                    timeout = value;
                }
            }
        }

        /// <summary>
        /// Returns the string representation of the object.
        /// </summary>
        /// <returns>String representation of the object.</returns>
        public override string ToString()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine("ADVANCED SETTINGS:");
            sb.AppendLine("==========================");
            sb.AppendFormat(System.Globalization.CultureInfo.InvariantCulture, "Timeout: {0}{1}", Timeout, System.Environment.NewLine);
            sb.AppendFormat(System.Globalization.CultureInfo.InvariantCulture, "Hidden: {0}{1}", IsHidden, System.Environment.NewLine);
            sb.AppendFormat(System.Globalization.CultureInfo.InvariantCulture, "Simulation: {0}{1}", IsSimulation, System.Environment.NewLine);
            sb.AppendFormat(System.Globalization.CultureInfo.InvariantCulture, "Read-only: {0}{1}", IsReadOnly, System.Environment.NewLine);
            return sb.ToString();
        }

        /// <summary>
        /// Loads the information to the component.
        /// </summary>
        /// <param name = "elementInfo">The element information.</param>
        internal override void Load(Skyline.DataMiner.Net.Messages.ElementInfoEventMessage elementInfo)
        {
            timeout = new System.TimeSpan(0, 0, 0, 0, elementInfo.ElementTimeoutTime);
            isHidden = elementInfo.Hidden;
            isReadOnly = elementInfo.IsReadOnly;
            isSimulation = elementInfo.IsSimulated;
        }
    }

    /// <summary>
    ///  Represents a class containing the device details of an element.
    /// </summary>
    internal class DeviceSettings : Skyline.DataMiner.Library.Common.ElementSettings
    {
        /// <summary>
        /// The type of the element.
        /// </summary>
        private string type = System.String.Empty;
        /// <summary>
        /// Initializes a new instance of the <see cref = "DeviceSettings"/> class.
        /// </summary>
        /// <param name = "dmsElement">The reference to the DmsElement where this object will be used in.</param>
        internal DeviceSettings(Skyline.DataMiner.Library.Common.DmsElement dmsElement): base(dmsElement)
        {
        }

        /// <summary>
        /// Returns the string representation of the object.
        /// </summary>
        /// <returns>String representation of the object.</returns>
        public override string ToString()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine("DEVICE SETTINGS:");
            sb.AppendLine("==========================");
            sb.AppendLine("Type: " + type);
            return sb.ToString();
        }

        /// <summary>
        /// Loads the information to the component.
        /// </summary>
        /// <param name = "elementInfo">The element information.</param>
        internal override void Load(Skyline.DataMiner.Net.Messages.ElementInfoEventMessage elementInfo)
        {
            type = elementInfo.Type ?? System.String.Empty;
        }
    }

    /// <summary>
    /// Represents DVE information of an element.
    /// </summary>
    internal class DveSettings : Skyline.DataMiner.Library.Common.ElementSettings, Skyline.DataMiner.Library.Common.IDveSettings
    {
        /// <summary>
        /// Value indicating whether DVE creation is enabled.
        /// </summary>
        private bool isDveCreationEnabled = true;
        /// <summary>
        /// Value indicating whether this element is a parent DVE.
        /// </summary>
        private bool isParent;
        /// <summary>
        /// The parent element.
        /// </summary>
        private Skyline.DataMiner.Library.Common.IDmsElement parent;
        /// <summary>
        /// Initializes a new instance of the <see cref = "DveSettings"/> class.
        /// </summary>
        /// <param name = "dmsElement">The reference to the DmsElement where this object will be used in.</param>
        internal DveSettings(Skyline.DataMiner.Library.Common.DmsElement dmsElement): base(dmsElement)
        {
        }

        /// <summary>
        /// Gets a value indicating whether this element is a DVE child.
        /// </summary>
        /// <value><c>true</c> if this element is a DVE child element; otherwise, <c>false</c>.</value>
        public bool IsChild
        {
            get
            {
                return parent != null;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether DVE creation is enabled for this element.
        /// </summary>
        /// <value><c>true</c> if the element DVE generation is enabled; otherwise, <c>false</c>.</value>
        /// <exception cref = "NotSupportedException">The set operation is not supported: The element is not a DVE parent element.</exception>
        public bool IsDveCreationEnabled
        {
            get
            {
                DmsElement.LoadOnDemand();
                return isDveCreationEnabled;
            }

            set
            {
                DmsElement.LoadOnDemand();
                if (!DmsElement.DveSettings.IsParent)
                {
                    throw new System.NotSupportedException("This operation is only supported on DVE parent elements.");
                }

                if (isDveCreationEnabled != value)
                {
                    ChangedPropertyList.Add("IsDveCreationEnabled");
                    isDveCreationEnabled = value;
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether this element is a DVE parent.
        /// </summary>
        /// <value><c>true</c> if the element is a DVE parent element; otherwise, <c>false</c>.</value>
        public bool IsParent
        {
            get
            {
                DmsElement.LoadOnDemand();
                return isParent;
            }
        }

        /// <summary>
        /// Returns the string representation of the object.
        /// </summary>
        /// <returns>String representation of the object.</returns>
        public override string ToString()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine("DVE SETTINGS:");
            sb.AppendLine("==========================");
            sb.AppendFormat(System.Globalization.CultureInfo.InvariantCulture, "DVE creation enabled: {0}{1}", IsDveCreationEnabled, System.Environment.NewLine);
            sb.AppendFormat(System.Globalization.CultureInfo.InvariantCulture, "Is parent DVE: {0}{1}", IsParent, System.Environment.NewLine);
            sb.AppendFormat(System.Globalization.CultureInfo.InvariantCulture, "Is child DVE: {0}{1}", IsChild, System.Environment.NewLine);
            if (IsChild)
            {
                sb.AppendFormat(System.Globalization.CultureInfo.InvariantCulture, "Parent DataMiner agent ID/element ID: {0}{1}", parent.DmsElementId.Value, System.Environment.NewLine);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Loads the information to the component.
        /// </summary>
        /// <param name = "elementInfo">The element information.</param>
        internal override void Load(Skyline.DataMiner.Net.Messages.ElementInfoEventMessage elementInfo)
        {
            if (elementInfo.IsDynamicElement && elementInfo.DveParentDmaId != 0 && elementInfo.DveParentElementId != 0)
            {
                parent = new Skyline.DataMiner.Library.Common.DmsElement(DmsElement.Dms, new Skyline.DataMiner.Library.Common.DmsElementId(elementInfo.DveParentDmaId, elementInfo.DveParentElementId));
            }

            isParent = elementInfo.IsDveMainElement;
            isDveCreationEnabled = elementInfo.CreateDVEs;
        }
    }

    /// <summary>
    /// Represents a class containing the failover settings for an element.
    /// </summary>
    internal class FailoverSettings : Skyline.DataMiner.Library.Common.ElementSettings, Skyline.DataMiner.Library.Common.IFailoverSettings
    {
        /// <summary>
        /// In failover configurations, this can be used to force an element to run only on one specific agent.
        /// </summary>
        private string forceAgent = System.String.Empty;
        /// <summary>
        /// Is true when the element is a failover element and is online on the backup agent instead of this agent; otherwise, false.
        /// </summary>
        private bool isOnlineOnBackupAgent;
        /// <summary>
        /// Is true when the element is a failover element that needs to keep running on the same DataMiner agent event after switching; otherwise, false.
        /// </summary>
        private bool keepOnline;
        /// <summary>
        /// Initializes a new instance of the <see cref = "FailoverSettings"/> class.
        /// </summary>
        /// <param name = "dmsElement">The reference to the DmsElement where this object will be used in.</param>
        internal FailoverSettings(Skyline.DataMiner.Library.Common.DmsElement dmsElement): base(dmsElement)
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether to force agent.
        /// Local IP address of the agent which will be running the element.
        /// </summary>
        /// <value>Value indicating whether to force agent.</value>
        public string ForceAgent
        {
            get
            {
                DmsElement.LoadOnDemand();
                return forceAgent;
            }

            set
            {
                DmsElement.LoadOnDemand();
                var newValue = value == null ? System.String.Empty : value;
                if (!forceAgent.Equals(newValue, System.StringComparison.Ordinal))
                {
                    ChangedPropertyList.Add("ForceAgent");
                    forceAgent = newValue;
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether the element is a failover element and is online on the backup agent instead of this agent.
        /// </summary>
        /// <value><c>true</c> if the element is a failover element and is online on the backup agent instead of this agent; otherwise, <c>false</c>.</value>
        public bool IsOnlineOnBackupAgent
        {
            get
            {
                DmsElement.LoadOnDemand();
                return isOnlineOnBackupAgent;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the element is a failover element that needs to keep running on the same DataMiner agent event after switching.
        /// keepOnline="true" indicates that the element needs to keep running even when the agent is offline.
        /// </summary>
        /// <value><c>true</c> if the element is a failover element that needs to keep running on the same DataMiner agent event after switching; otherwise, <c>false</c>.</value>
        public bool KeepOnline
        {
            get
            {
                DmsElement.LoadOnDemand();
                return keepOnline;
            }

            set
            {
                DmsElement.LoadOnDemand();
                if (keepOnline != value)
                {
                    ChangedPropertyList.Add("KeepOnline");
                    keepOnline = value;
                }
            }
        }

        /// <summary>
        /// Returns the string representation of the object.
        /// </summary>
        /// <returns>String representation of the object.</returns>
        public override string ToString()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine("FAILOVER SETTINGS:");
            sb.AppendLine("==========================");
            sb.AppendFormat(System.Globalization.CultureInfo.InvariantCulture, "Keep online: {0}{1}", KeepOnline, System.Environment.NewLine);
            sb.AppendFormat(System.Globalization.CultureInfo.InvariantCulture, "Force agent: {0}{1}", ForceAgent, System.Environment.NewLine);
            sb.AppendFormat(System.Globalization.CultureInfo.InvariantCulture, "Online on backup agent: {0}{1}", IsOnlineOnBackupAgent, System.Environment.NewLine);
            return sb.ToString();
        }

        /// <summary>
        /// Loads the information to the component.
        /// </summary>
        /// <param name = "elementInfo">The element information.</param>
        internal override void Load(Skyline.DataMiner.Net.Messages.ElementInfoEventMessage elementInfo)
        {
            keepOnline = elementInfo.KeepOnline;
            forceAgent = elementInfo.ForceAgent ?? System.String.Empty;
            isOnlineOnBackupAgent = elementInfo.IsOnlineOnBackupAgent;
        }
    }

    /// <summary>
    /// Represents general element information.
    /// </summary>
    internal class GeneralSettings : Skyline.DataMiner.Library.Common.ElementSettings
    {
        /// <summary>
        /// The name of the alarm template.
        /// </summary>
        private string alarmTemplateName;
        /// <summary>
        /// The alarm template assigned to this element.
        /// </summary>
        private Skyline.DataMiner.Library.Common.Templates.IDmsAlarmTemplate alarmTemplate;
        /// <summary>
        /// Element description.
        /// </summary>
        private string description = System.String.Empty;
        /// <summary>
        /// The hosting DataMiner agent.
        /// </summary>
        private Skyline.DataMiner.Library.Common.Dma host;
        /// <summary>
        /// The element state.
        /// </summary>
        private Skyline.DataMiner.Library.Common.ElementState state = Skyline.DataMiner.Library.Common.ElementState.Active;
        /// <summary>
        /// Instance of the protocol this element executes.
        /// </summary>
        private Skyline.DataMiner.Library.Common.DmsProtocol protocol;
        /// <summary>
        /// The trend template assigned to this element.
        /// </summary>
        private Skyline.DataMiner.Library.Common.Templates.IDmsTrendTemplate trendTemplate;
        /// <summary>
        /// The name of the element.
        /// </summary>
        private string name;
        /// <summary>
        /// Initializes a new instance of the <see cref = "GeneralSettings"/> class.
        /// </summary>
        /// <param name = "dmsElement">The reference to the DmsElement where this object will be used in.</param>
        internal GeneralSettings(Skyline.DataMiner.Library.Common.DmsElement dmsElement): base(dmsElement)
        {
        }

        /// <summary>
        /// Gets or sets the alarm template definition of the element.
        /// This can either be an alarm template or an alarm template group.
        /// </summary>
        internal Skyline.DataMiner.Library.Common.Templates.IDmsAlarmTemplate AlarmTemplate
        {
            get
            {
                DmsElement.LoadOnDemand();
                return alarmTemplate;
            }

            set
            {
                DmsElement.LoadOnDemand();
                bool updateRequired = false;
                if (alarmTemplate == null)
                {
                    if (value != null)
                    {
                        updateRequired = true;
                    }
                }
                else
                {
                    if (value == null || !alarmTemplate.Equals(value))
                    {
                        updateRequired = true;
                    }
                }

                if (updateRequired)
                {
                    ChangedPropertyList.Add("AlarmTemplate");
                    alarmTemplateName = value == null ? System.String.Empty : value.Name;
                    alarmTemplate = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the element description.
        /// </summary>
        internal string Description
        {
            get
            {
                DmsElement.LoadOnDemand();
                return description;
            }

            set
            {
                DmsElement.LoadOnDemand();
                string newValue = value == null ? System.String.Empty : value;
                if (!description.Equals(newValue, System.StringComparison.Ordinal))
                {
                    ChangedPropertyList.Add("Description");
                    description = newValue;
                }
            }
        }

        /// <summary>
        /// Gets or sets the system-wide element ID.
        /// </summary>
        internal Skyline.DataMiner.Library.Common.DmsElementId DmsElementId
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the DataMiner agent that hosts the element.
        /// </summary>
        internal Skyline.DataMiner.Library.Common.Dma Host
        {
            get
            {
                DmsElement.LoadOnDemand();
                return host;
            }
        }

        /// <summary>
        /// Gets or sets the state of the element.
        /// </summary>
        internal Skyline.DataMiner.Library.Common.ElementState State
        {
            get
            {
                DmsElement.LoadOnDemand();
                return state;
            }

            set
            {
                DmsElement.LoadOnDemand();
                state = value;
            }
        }

        /// <summary>
        /// Gets or sets the trend template assigned to this element.
        /// </summary>
        internal Skyline.DataMiner.Library.Common.Templates.IDmsTrendTemplate TrendTemplate
        {
            get
            {
                DmsElement.LoadOnDemand();
                return trendTemplate;
            }

            set
            {
                DmsElement.LoadOnDemand();
                bool updateRequired = false;
                if (trendTemplate == null)
                {
                    if (value != null)
                    {
                        updateRequired = true;
                    }
                }
                else
                {
                    if (value == null || !trendTemplate.Equals(value))
                    {
                        updateRequired = true;
                    }
                }

                if (updateRequired)
                {
                    ChangedPropertyList.Add("TrendTemplate");
                    trendTemplate = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the name of the element.
        /// </summary>
        /// <exception cref = "NotSupportedException">A set operation is not supported on a DVE child or a derived element.</exception>
        internal string Name
        {
            get
            {
                DmsElement.LoadOnDemand();
                return name;
            }

            set
            {
                DmsElement.LoadOnDemand();
                if (DmsElement.DveSettings.IsChild || DmsElement.RedundancySettings.IsDerived)
                {
                    throw new System.NotSupportedException("Setting the name of a DVE child or a derived element is not supported.");
                }

                if (!name.Equals(value, System.StringComparison.Ordinal))
                {
                    ChangedPropertyList.Add("Name");
                    name = value.Trim();
                }
            }
        }

        /// <summary>
        /// Gets or sets the instance of the protocol.
        /// </summary>
        /// <exception cref = "ArgumentNullException">The value of a set operation is <see langword = "null"/>.</exception>
        /// <exception cref = "ArgumentException">The value of a set operation is empty.</exception>
        internal Skyline.DataMiner.Library.Common.DmsProtocol Protocol
        {
            get
            {
                DmsElement.LoadOnDemand();
                return protocol;
            }

            set
            {
                if (value == null)
                {
                    throw new System.ArgumentNullException("value");
                }

                DmsElement.LoadOnDemand();
                ChangedPropertyList.Add("Protocol");
                protocol = value;
            }
        }

        /// <summary>
        /// Returns the string representation of the object.
        /// </summary>
        /// <returns>String representation of the object.</returns>
        public override string ToString()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine("GENERAL SETTINGS:");
            sb.AppendLine("==========================");
            sb.AppendFormat(System.Globalization.CultureInfo.InvariantCulture, "Name: {0}{1}", DmsElement.Name, System.Environment.NewLine);
            sb.AppendFormat(System.Globalization.CultureInfo.InvariantCulture, "Description: {0}{1}", Description, System.Environment.NewLine);
            sb.AppendFormat(System.Globalization.CultureInfo.InvariantCulture, "Protocol name: {0}{1}", Protocol.Name, System.Environment.NewLine);
            sb.AppendFormat(System.Globalization.CultureInfo.InvariantCulture, "Protocol version: {0}{1}", Protocol.Version, System.Environment.NewLine);
            sb.AppendFormat(System.Globalization.CultureInfo.InvariantCulture, "DMA ID: {0}{1}", DmsElementId.AgentId, System.Environment.NewLine);
            sb.AppendFormat(System.Globalization.CultureInfo.InvariantCulture, "Element ID: {0}{1}", DmsElementId.ElementId, System.Environment.NewLine);
            sb.AppendFormat(System.Globalization.CultureInfo.InvariantCulture, "Hosting DMA ID: {0}{1}", Host.Id, System.Environment.NewLine);
            sb.AppendFormat(System.Globalization.CultureInfo.InvariantCulture, "Alarm template: {0}{1}", AlarmTemplate, System.Environment.NewLine);
            sb.AppendFormat(System.Globalization.CultureInfo.InvariantCulture, "Trend template: {0}{1}", TrendTemplate, System.Environment.NewLine);
            sb.AppendFormat(System.Globalization.CultureInfo.InvariantCulture, "State: {0}{1}", State, System.Environment.NewLine);
            return sb.ToString();
        }

        /// <summary>
        /// Loads the information to the component.
        /// </summary>
        /// <param name = "elementInfo">The element information.</param>
        internal override void Load(Skyline.DataMiner.Net.Messages.ElementInfoEventMessage elementInfo)
        {
            DmsElementId = new Skyline.DataMiner.Library.Common.DmsElementId(elementInfo.DataMinerID, elementInfo.ElementID);
            description = elementInfo.Description ?? System.String.Empty;
            protocol = new Skyline.DataMiner.Library.Common.DmsProtocol(DmsElement.Dms, elementInfo.Protocol, elementInfo.ProtocolVersion);
            alarmTemplateName = elementInfo.ProtocolTemplate;
            trendTemplate = System.String.IsNullOrWhiteSpace(elementInfo.Trending) ? null : new Skyline.DataMiner.Library.Common.Templates.DmsTrendTemplate(DmsElement.Dms, elementInfo.Trending, protocol);
            state = (Skyline.DataMiner.Library.Common.ElementState)elementInfo.State;
            name = elementInfo.Name ?? System.String.Empty;
            host = new Skyline.DataMiner.Library.Common.Dma(DmsElement.Dms, elementInfo.HostingAgentID);
            LoadAlarmTemplateDefinition();
        }

        /// <summary>
        /// Loads the alarm template definition.
        /// This method checks whether there is a group or a template assigned to the element.
        /// </summary>
        private void LoadAlarmTemplateDefinition()
        {
            if (alarmTemplate == null && !System.String.IsNullOrWhiteSpace(alarmTemplateName))
            {
                Skyline.DataMiner.Net.Messages.GetAlarmTemplateMessage message = new Skyline.DataMiner.Net.Messages.GetAlarmTemplateMessage{AsOneObject = true, Protocol = protocol.Name, Version = protocol.Version, Template = alarmTemplateName};
                Skyline.DataMiner.Net.Messages.AlarmTemplateEventMessage response = (Skyline.DataMiner.Net.Messages.AlarmTemplateEventMessage)DmsElement.Dms.Communication.SendSingleResponseMessage(message);
                if (response != null)
                {
                    switch (response.Type)
                    {
                        case Skyline.DataMiner.Net.Messages.AlarmTemplateType.Template:
                            alarmTemplate = new Skyline.DataMiner.Library.Common.Templates.DmsStandaloneAlarmTemplate(DmsElement.Dms, response);
                            break;
                        case Skyline.DataMiner.Net.Messages.AlarmTemplateType.Group:
                            alarmTemplate = new Skyline.DataMiner.Library.Common.Templates.DmsAlarmTemplateGroup(DmsElement.Dms, response);
                            break;
                        default:
                            throw new System.InvalidOperationException("Unexpected value: " + response.Type);
                    }
                }
            }
        }
    }

    /// <summary>
    /// DataMiner element advanced settings interface.
    /// </summary>
    public interface IAdvancedSettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether the element is hidden.
        /// </summary>
        /// <value><c>true</c> if the element is hidden; otherwise, <c>false</c>.</value>
        /// <exception cref = "NotSupportedException">A set operation is not supported on a derived element.</exception>
        bool IsHidden
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the element is read-only.
        /// </summary>
        /// <value><c>true</c> if the element is read-only; otherwise, <c>false</c>.</value>
        /// <exception cref = "NotSupportedException">A set operation is not supported on a DVE or derived element.</exception>
        bool IsReadOnly
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a value indicating whether the element is running a simulation.
        /// </summary>
        /// <value><c>true</c> if the element is running a simulation; otherwise, <c>false</c>.</value>
        bool IsSimulation
        {
            get;
        }

        /// <summary>
        /// Gets or sets the element timeout value.
        /// </summary>
        /// <value>The timeout value.</value>
        /// <exception cref = "NotSupportedException">A set operation is not supported on a DVE or derived element.</exception>
        /// <exception cref = "ArgumentOutOfRangeException">The value specified for a set operation is not in the range of [0,120] s.</exception>
        /// <remarks>Fractional seconds are ignored. For example, setting the timeout to a value of 3.5s results in setting it to 3s.</remarks>
        System.TimeSpan Timeout
        {
            get;
            set;
        }
    }

    /// <summary>
    /// DataMiner element DVE settings interface.
    /// </summary>
    public interface IDveSettings
    {
        /// <summary>
        /// Gets a value indicating whether this element is a DVE child.
        /// </summary>
        /// <value><c>true</c> if this element is a DVE child element; otherwise, <c>false</c>.</value>
        bool IsChild
        {
            get;
        }

        /// <summary>
        /// Gets or sets a value indicating whether DVE creation is enabled for this element.
        /// </summary>
        /// <value><c>true</c> if the element DVE generation is enabled; otherwise, <c>false</c>.</value>
        /// <exception cref = "NotSupportedException">The element is not a DVE parent element.</exception>
        bool IsDveCreationEnabled
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a value indicating whether this element is a DVE parent.
        /// </summary>
        /// <value><c>true</c> if the element is a DVE parent element; otherwise, <c>false</c>.</value>
        bool IsParent
        {
            get;
        }
    }

    /// <summary>
    /// DataMiner element failover settings interface.
    /// </summary>
    internal interface IFailoverSettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether to force agent.
        /// Local IP address of the agent which will be running the element.
        /// </summary>
        /// <value>Value indicating whether to force agent.</value>
        string ForceAgent
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a value indicating whether the element is a failover element and is online on the backup agent instead of this agent.
        /// </summary>
        /// <value><c>true</c> if the element is a failover element and is online on the backup agent instead of this agent; otherwise, <c>false</c>.</value>
        bool IsOnlineOnBackupAgent
        {
            get;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the element is a failover element that needs to keep running on the same DataMiner agent event after switching.
        /// </summary>
        /// <value><c>true</c> if the element is a failover element that needs to keep running on the same DataMiner agent event after switching; otherwise, <c>false</c>.</value>
        bool KeepOnline
        {
            get;
            set;
        }
    }

    /// <summary>
    /// DataMiner element redundancy settings interface.
    /// </summary>
    public interface IRedundancySettings
    {
        /// <summary>
        /// Gets a value indicating whether the element is derived from another element.
        /// </summary>
        /// <value><c>true</c> if the element is derived from another element; otherwise, <c>false</c>.</value>
        bool IsDerived
        {
            get;
        }
    }

    /// <summary>
    /// DataMiner element replication settings interface.
    /// </summary>
    public interface IReplicationSettings
    {
    }

    /// <summary>
    /// Represents the redundancy settings for a element.
    /// </summary>
    internal class RedundancySettings : Skyline.DataMiner.Library.Common.ElementSettings, Skyline.DataMiner.Library.Common.IRedundancySettings
    {
        /// <summary>
        /// Value indicating whether or not this element is derived from another element.
        /// </summary>
        private bool isDerived;
        /// <summary>
        /// Initializes a new instance of the <see cref = "RedundancySettings"/> class.
        /// </summary>
        /// <param name = "dmsElement">The reference to the <see cref = "DmsElement"/> instance this object is part of.</param>
        internal RedundancySettings(Skyline.DataMiner.Library.Common.DmsElement dmsElement): base(dmsElement)
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether the element is derived from another element.
        /// </summary>
        /// <value><c>true</c> if the element is derived from another element; otherwise, <c>false</c>.</value>
        public bool IsDerived
        {
            get
            {
                DmsElement.LoadOnDemand();
                return isDerived;
            }

            internal set
            {
                isDerived = value;
            }
        }

        /// <summary>
        /// Returns the string representation of the object.
        /// </summary>
        /// <returns>String representation of the object.</returns>
        public override string ToString()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine("REDUNDANCY SETTINGS:");
            sb.AppendLine("==========================");
            sb.AppendFormat(System.Globalization.CultureInfo.InvariantCulture, "Derived: {0}{1}", isDerived, System.Environment.NewLine);
            return sb.ToString();
        }

        /// <summary>
        /// Loads the information to the component.
        /// </summary>
        /// <param name = "elementInfo">The element information.</param>
        internal override void Load(Skyline.DataMiner.Net.Messages.ElementInfoEventMessage elementInfo)
        {
            isDerived = elementInfo.IsDerivedElement;
        }
    }

    /// <summary>
    /// Represents the replication information of an element.
    /// </summary>
    internal class ReplicationSettings : Skyline.DataMiner.Library.Common.ElementSettings, Skyline.DataMiner.Library.Common.IReplicationSettings
    {
        /// <summary>
        /// The domain the specified user belongs to.
        /// </summary>
        private string domain = System.String.Empty;
        /// <summary>
        /// External DMP engine.
        /// </summary>
        private bool connectsToExternalDmp;
        /// <summary>
        /// IP address of the source DataMiner Agent.
        /// </summary>
        private string ipAddressSourceDma = System.String.Empty;
        /// <summary>
        /// Value indicating whether this element is replicated.
        /// </summary>
        private bool isReplicated;
        /// <summary>
        /// The options string.
        /// </summary>
        private string options = System.String.Empty;
        /// <summary>
        /// The password.
        /// </summary>
        private string password = System.String.Empty;
        /// <summary>
        /// The ID of the source element.
        /// </summary>
        private Skyline.DataMiner.Library.Common.DmsElementId sourceDmsElementId = new Skyline.DataMiner.Library.Common.DmsElementId(-1, -1);
        /// <summary>
        /// The user name.
        /// </summary>
        private string userName = System.String.Empty;
        /// <summary>
        /// Initializes a new instance of the <see cref = "ReplicationSettings"/> class.
        /// </summary>
        /// <param name = "dmsElement">The reference to the DmsElement where this object will be used in.</param>
        internal ReplicationSettings(Skyline.DataMiner.Library.Common.DmsElement dmsElement): base(dmsElement)
        {
        }

        /// <summary>
        /// Returns the string representation of the object.
        /// </summary>
        /// <returns>String representation of the object.</returns>
        public override string ToString()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine("REPLICATION SETTINGS:");
            sb.AppendLine("==========================");
            sb.AppendFormat(System.Globalization.CultureInfo.InvariantCulture, "Replicated: {0}{1}", isReplicated, System.Environment.NewLine);
            sb.AppendFormat(System.Globalization.CultureInfo.InvariantCulture, "Source DMA ID: {0}{1}", sourceDmsElementId.AgentId, System.Environment.NewLine);
            sb.AppendFormat(System.Globalization.CultureInfo.InvariantCulture, "Source element ID: {0}{1}", sourceDmsElementId.ElementId, System.Environment.NewLine);
            sb.AppendFormat(System.Globalization.CultureInfo.InvariantCulture, "IP address source DMA: {0}{1}", ipAddressSourceDma, System.Environment.NewLine);
            sb.AppendFormat(System.Globalization.CultureInfo.InvariantCulture, "Domain: {0}{1}", domain, System.Environment.NewLine);
            sb.AppendFormat(System.Globalization.CultureInfo.InvariantCulture, "User name: {0}{1}", userName, System.Environment.NewLine);
            sb.AppendFormat(System.Globalization.CultureInfo.InvariantCulture, "Password: {0}{1}", password, System.Environment.NewLine);
            //sb.AppendFormat(CultureInfo.InvariantCulture, "Options: {0}{1}", options, Environment.NewLine);
            //sb.AppendFormat(CultureInfo.InvariantCulture, "Replication DMP engine: {0}{1}", connectsToExternalDmp, Environment.NewLine);
            return sb.ToString();
        }

        /// <summary>
        /// Loads the information to the component.
        /// </summary>
        /// <param name = "elementInfo">The element information.</param>
        internal override void Load(Skyline.DataMiner.Net.Messages.ElementInfoEventMessage elementInfo)
        {
            isReplicated = elementInfo.ReplicationActive;
            if (!isReplicated)
            {
                options = System.String.Empty;
                ipAddressSourceDma = System.String.Empty;
                password = System.String.Empty;
                domain = System.String.Empty;
                sourceDmsElementId = new Skyline.DataMiner.Library.Common.DmsElementId(-1, -1);
                userName = System.String.Empty;
                connectsToExternalDmp = false;
            }

            options = elementInfo.ReplicationOptions ?? System.String.Empty;
            ipAddressSourceDma = elementInfo.ReplicationDmaIP ?? System.String.Empty;
            password = elementInfo.ReplicationPwd ?? System.String.Empty;
            domain = elementInfo.ReplicationDomain ?? System.String.Empty;
            bool isEmpty = System.String.IsNullOrWhiteSpace(elementInfo.ReplicationRemoteElement) || elementInfo.ReplicationRemoteElement.Equals("/", System.StringComparison.Ordinal);
            if (isEmpty)
            {
                sourceDmsElementId = new Skyline.DataMiner.Library.Common.DmsElementId(-1, -1);
            }
            else
            {
                try
                {
                    sourceDmsElementId = new Skyline.DataMiner.Library.Common.DmsElementId(elementInfo.ReplicationRemoteElement);
                }
                catch (System.Exception ex)
                {
                    string logMessage = "Failed parsing replication element info for element " + System.Convert.ToString(elementInfo.Name) + " (" + System.Convert.ToString(elementInfo.DataMinerID) + "/" + System.Convert.ToString(elementInfo.ElementID) + "). Replication remote element is: " + System.Convert.ToString(elementInfo.ReplicationRemoteElement) + System.Environment.NewLine + ex;
                    Skyline.DataMiner.Library.Common.Logger.Log(logMessage);
                    sourceDmsElementId = new Skyline.DataMiner.Library.Common.DmsElementId(-1, -1);
                }
            }

            userName = elementInfo.ReplicationUser ?? System.String.Empty;
            connectsToExternalDmp = elementInfo.ReplicationIsExternalDMP;
        }
    }

    /// <summary>
    /// Represents a base class for all of the components in a DmsElement object.
    /// </summary>
    internal abstract class ElementSettings
    {
        /// <summary>
        /// The list of changed properties.
        /// </summary>
        private readonly System.Collections.Generic.List<System.String> changedPropertyList = new System.Collections.Generic.List<System.String>();
        /// <summary>
        /// Instance of the DmsElement class where these classes will be used for.
        /// </summary>
        private readonly Skyline.DataMiner.Library.Common.DmsElement dmsElement;
        /// <summary>
        /// Initializes a new instance of the <see cref = "ElementSettings"/> class.
        /// </summary>
        /// <param name = "dmsElement">The reference to the <see cref = "DmsElement"/> instance this object is part of.</param>
        protected ElementSettings(Skyline.DataMiner.Library.Common.DmsElement dmsElement)
        {
            this.dmsElement = dmsElement;
        }

        /// <summary>
        /// Gets the element this object belongs to.
        /// </summary>
        internal Skyline.DataMiner.Library.Common.DmsElement DmsElement
        {
            get
            {
                return dmsElement;
            }
        }

        /// <summary>
        /// Gets the list of updated properties.
        /// </summary>
        protected internal System.Collections.Generic.List<System.String> ChangedPropertyList
        {
            get
            {
                return changedPropertyList;
            }
        }

        /// <summary>
        /// Based on the array provided from the DmsNotify call, parse the data to the correct fields.
        /// </summary>
        /// <param name = "elementInfo">Object containing all the required information. Retrieved by DmsClass.</param>
        internal abstract void Load(Skyline.DataMiner.Net.Messages.ElementInfoEventMessage elementInfo);
    }

    /// <summary>
    /// Represents a DataMiner protocol.
    /// </summary>
    internal class DmsProtocol : Skyline.DataMiner.Library.Common.DmsObject, Skyline.DataMiner.Library.Common.IDmsProtocol
    {
        /// <summary>
        /// The constant value 'Production'.
        /// </summary>
        private const string Production = "Production";
        /// <summary>
        /// The protocol name.
        /// </summary>
        private string name;
        /// <summary>
        /// The protocol version.
        /// </summary>
        private string version;
        /// <summary>
        /// The protocol referenced version.
        /// </summary>
        private string referencedVersion = null;
        /// <summary>
        /// Whether the version is 'Production'.
        /// </summary>
        private bool isProduction;
        /// <summary>
        /// The connection info of the protocol.
        /// </summary>
        private System.Collections.Generic.IList<Skyline.DataMiner.Library.Common.IDmsConnectionInfo> connectionInfo = new System.Collections.Generic.List<Skyline.DataMiner.Library.Common.IDmsConnectionInfo>();
        /// <summary>
        /// Initializes a new instance of the <see cref = "DmsProtocol"/> class.
        /// </summary>
        /// <param name = "dms">The DataMiner System.</param>
        /// <param name = "name">The protocol name.</param>
        /// <param name = "version">The protocol version.</param>
        /// <param name = "referencedVersion">The protocol referenced version.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "dms"/> is <see langword = "null"/>.</exception>
        /// <exception cref = "ArgumentNullException"><paramref name = "name"/> is <see langword = "null"/>.</exception>
        /// <exception cref = "ArgumentNullException"><paramref name = "version"/> is <see langword = "null"/>.</exception>
        /// <exception cref = "ArgumentException"><paramref name = "name"/> is the empty string ("") or white space.</exception>
        /// <exception cref = "ArgumentException"><paramref name = "version"/> is the empty string ("") or white space.</exception>
        /// <exception cref = "ArgumentException"><paramref name = "version"/> is not 'Production' and <paramref name = "referencedVersion"/> is not the empty string ("") or white space.</exception>
        internal DmsProtocol(Skyline.DataMiner.Library.Common.IDms dms, string name, string version, string referencedVersion = ""): base(dms)
        {
            if (name == null)
            {
                throw new System.ArgumentNullException("name");
            }

            if (version == null)
            {
                throw new System.ArgumentNullException("version");
            }

            if (System.String.IsNullOrWhiteSpace(name))
            {
                throw new System.ArgumentException("The name of the protocol is the empty string (\"\") or white space.", "name");
            }

            if (System.String.IsNullOrWhiteSpace(version))
            {
                throw new System.ArgumentException("The version of the protocol is the empty string (\"\") or white space.", "version");
            }

            this.name = name;
            this.version = version;
            this.isProduction = CheckIsProduction(this.version);
            if (!this.isProduction && !System.String.IsNullOrWhiteSpace(referencedVersion))
            {
                throw new System.ArgumentException("The version of the protocol is not referenced version of the protocol is not the empty string (\"\") or white space.", "referencedVersion");
            }

            this.referencedVersion = referencedVersion;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref = "DmsProtocol"/> class.
        /// </summary>
        /// <param name = "dms">The DataMiner system.</param>
        /// <param name = "infoMessage">The information message received from SLNet.</param>
        /// <param name = "requestedProduction">The version requested to SLNet.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "infoMessage"/> is <see langword = "null"/>.</exception>
        internal DmsProtocol(Skyline.DataMiner.Library.Common.IDms dms, Skyline.DataMiner.Net.Messages.GetProtocolInfoResponseMessage infoMessage, bool requestedProduction): base(dms)
        {
            if (infoMessage == null)
            {
                throw new System.ArgumentNullException("infoMessage");
            }

            this.isProduction = requestedProduction;
            Parse(infoMessage);
        }

        /// <summary>
        /// Gets the protocol name.
        /// </summary>
        /// <value>The protocol name.</value>
        public string Name
        {
            get
            {
                return name;
            }
        }

        /// <summary>
        /// Gets the protocol version.
        /// </summary>
        /// <value>The protocol version.</value>
        public string Version
        {
            get
            {
                return version;
            }
        }

        /// <summary>
        /// Gets the alarm template with the specified name defined for this protocol.
        /// </summary>
        /// <param name = "templateName">The name of the alarm template.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "templateName"/> is <see langword = "null"/>.</exception>
        /// <exception cref = "ArgumentException"><paramref name = "templateName"/> is the empty string ("") or white space.</exception>
        /// <exception cref = "AlarmTemplateNotFoundException">No alarm template with the specified name was found.</exception>
        /// <returns>The alarm template with the specified name defined for this protocol.</returns>
        public Skyline.DataMiner.Library.Common.Templates.IDmsAlarmTemplate GetAlarmTemplate(string templateName)
        {
            Skyline.DataMiner.Net.Messages.GetAlarmTemplateMessage message = new Skyline.DataMiner.Net.Messages.GetAlarmTemplateMessage{AsOneObject = true, Protocol = this.Name, Version = this.Version, Template = templateName};
            Skyline.DataMiner.Net.Messages.AlarmTemplateEventMessage alarmTemplateEventMessage = (Skyline.DataMiner.Net.Messages.AlarmTemplateEventMessage)dms.Communication.SendSingleResponseMessage(message);
            if (alarmTemplateEventMessage == null)
            {
                throw new Skyline.DataMiner.Library.Common.AlarmTemplateNotFoundException(templateName, this);
            }

            if (alarmTemplateEventMessage.Type == Skyline.DataMiner.Net.Messages.AlarmTemplateType.Template)
            {
                return new Skyline.DataMiner.Library.Common.Templates.DmsStandaloneAlarmTemplate(dms, alarmTemplateEventMessage);
            }
            else if (alarmTemplateEventMessage.Type == Skyline.DataMiner.Net.Messages.AlarmTemplateType.Group)
            {
                return new Skyline.DataMiner.Library.Common.Templates.DmsAlarmTemplateGroup(dms, alarmTemplateEventMessage);
            }
            else
            {
                throw new System.NotSupportedException("Support for " + alarmTemplateEventMessage.Type + " has not yet been implemented.");
            }
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return System.String.Format(System.Globalization.CultureInfo.InvariantCulture, "Protocol name: {0}, version: {1}", Name, Version);
        }

        /// <summary>
        /// Loads the object.
        /// </summary>
        /// <exception cref = "ProtocolNotFoundException">No protocol with the specified name and version exists in the DataMiner system.</exception>
        internal override void Load()
        {
            isProduction = CheckIsProduction(version);
            Skyline.DataMiner.Net.Messages.GetProtocolMessage getProtocolMessage = new Skyline.DataMiner.Net.Messages.GetProtocolMessage{Protocol = name, Version = version};
            Skyline.DataMiner.Net.Messages.GetProtocolInfoResponseMessage protocolInfo = (Skyline.DataMiner.Net.Messages.GetProtocolInfoResponseMessage)Communication.SendSingleResponseMessage(getProtocolMessage);
            if (protocolInfo != null)
            {
                Parse(protocolInfo);
            }
            else
            {
                throw new Skyline.DataMiner.Library.Common.ProtocolNotFoundException(name, version);
            }
        }

        /// <summary>
        /// Parses the <see cref = "GetProtocolInfoResponseMessage"/> message.
        /// </summary>
        /// <param name = "protocolInfo">The protocol information.</param>
        private void Parse(Skyline.DataMiner.Net.Messages.GetProtocolInfoResponseMessage protocolInfo)
        {
            IsLoaded = true;
            name = protocolInfo.Name;
            if (isProduction)
            {
                version = Production;
                referencedVersion = protocolInfo.Version;
            }
            else
            {
                version = protocolInfo.Version;
                referencedVersion = System.String.Empty;
            }

            ParseConnectionInfo(protocolInfo);
        }

        /// <summary>
        /// Parses the <see cref = "GetProtocolInfoResponseMessage"/> message.
        /// </summary>
        /// <param name = "protocolInfo">The protocol information.</param>
        private void ParseConnectionInfo(Skyline.DataMiner.Net.Messages.GetProtocolInfoResponseMessage protocolInfo)
        {
            System.Collections.Generic.List<Skyline.DataMiner.Library.Common.DmsConnectionInfo> info = new System.Collections.Generic.List<Skyline.DataMiner.Library.Common.DmsConnectionInfo>();
            info.Add(new Skyline.DataMiner.Library.Common.DmsConnectionInfo(System.String.Empty, Skyline.DataMiner.Library.Common.EnumMapper.ConvertStringToConnectionType(protocolInfo.Type)));
            if (protocolInfo.AdvancedTypes != null && protocolInfo.AdvancedTypes.Length > 0 && !System.String.IsNullOrWhiteSpace(protocolInfo.AdvancedTypes))
            {
                string[] split = protocolInfo.AdvancedTypes.Split(';');
                foreach (string part in split)
                {
                    if (part.Contains(":"))
                    {
                        string[] connectionSplit = part.Split(':');
                        Skyline.DataMiner.Library.Common.ConnectionType connectionType = Skyline.DataMiner.Library.Common.EnumMapper.ConvertStringToConnectionType(connectionSplit[0]);
                        string connectionName = connectionSplit[1];
                        info.Add(new Skyline.DataMiner.Library.Common.DmsConnectionInfo(connectionName, connectionType));
                    }
                    else
                    {
                        Skyline.DataMiner.Library.Common.ConnectionType connectionType = Skyline.DataMiner.Library.Common.EnumMapper.ConvertStringToConnectionType(part);
                        string connectionName = System.String.Empty;
                        info.Add(new Skyline.DataMiner.Library.Common.DmsConnectionInfo(connectionName, connectionType));
                    }
                }
            }

            connectionInfo = info.ToArray();
        }

        /// <summary>
        /// Validate if <paramref name = "version"/> is 'Production'.
        /// </summary>
        /// <param name = "version">The version.</param>
        /// <returns>Whether <paramref name = "version"/> is 'Production'.</returns>
        internal static bool CheckIsProduction(string version)
        {
            return System.String.Equals(version, Production, System.StringComparison.OrdinalIgnoreCase);
        }
    }

    /// <summary>
    /// DataMiner protocol interface.
    /// </summary>
    public interface IDmsProtocol : Skyline.DataMiner.Library.Common.IDmsObject
    {
        /// <summary>
        /// Gets the protocol name.
        /// </summary>
        /// <value>The protocol name.</value>
        string Name
        {
            get;
        }

        /// <summary>
        /// Gets the protocol version.
        /// </summary>
        /// <value>The protocol version.</value>
        string Version
        {
            get;
        }

        /// <summary>
        /// Gets the alarm template with the specified name defined for this protocol.
        /// </summary>
        /// <param name = "templateName">The name of the alarm template.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "templateName"/> is <see langword = "null"/>.</exception>
        /// <exception cref = "ArgumentException"><paramref name = "templateName"/> is the empty string ("") or white space.</exception>
        /// <exception cref = "AlarmTemplateNotFoundException">No alarm template with the specified name was found.</exception>
        /// <returns>The alarm template with the specified name defined for this protocol.</returns>
        Skyline.DataMiner.Library.Common.Templates.IDmsAlarmTemplate GetAlarmTemplate(string templateName);
    }

    namespace Templates
    {
        /// <summary>
        /// Base class for standalone alarm templates and alarm template groups.
        /// </summary>
        internal abstract class DmsAlarmTemplate : Skyline.DataMiner.Library.Common.Templates.DmsTemplate, Skyline.DataMiner.Library.Common.Templates.IDmsAlarmTemplate
        {
            /// <summary>
            /// Initializes a new instance of the <see cref = "DmsAlarmTemplate"/> class.
            /// </summary>
            /// <param name = "dms">Object implementing the <see cref = "IDms"/> interface.</param>
            /// <param name = "name">The name of the alarm template.</param>
            /// <param name = "protocol">Instance of the protocol.</param>
            /// <exception cref = "ArgumentNullException"><paramref name = "dms"/> is <see langword = "null"/>.</exception>
            /// <exception cref = "ArgumentNullException"><paramref name = "name"/> is <see langword = "null"/>.</exception>
            /// <exception cref = "ArgumentNullException"><paramref name = "protocol"/> is <see langword = "null"/>.</exception>
            /// <exception cref = "ArgumentException"><paramref name = "name"/> is the empty string ("") or white space.</exception>
            protected DmsAlarmTemplate(Skyline.DataMiner.Library.Common.IDms dms, string name, Skyline.DataMiner.Library.Common.DmsProtocol protocol): base(dms, name, protocol)
            {
            }

            /// <summary>
            /// Initializes a new instance of the <see cref = "DmsAlarmTemplate"/> class.
            /// </summary>
            /// <param name = "dms">Object implementing the <see cref = "IDms"/> interface.</param>
            /// <param name = "name">The name of the alarm template.</param>
            /// <param name = "protocolName">The name of the protocol.</param>
            /// <param name = "protocolVersion">The version of the protocol.</param>
            /// <exception cref = "ArgumentNullException"><paramref name = "dms"/> is <see langword = "null"/>.</exception>
            /// <exception cref = "ArgumentNullException"><paramref name = "name"/> is <see langword = "null"/>.</exception>
            /// <exception cref = "ArgumentNullException"><paramref name = "protocolName"/> is <see langword = "null"/>.</exception>
            /// <exception cref = "ArgumentNullException"><paramref name = "protocolVersion"/> is <see langword = "null"/>.</exception>
            /// <exception cref = "ArgumentException"><paramref name = "name"/> is the empty string ("") or white space.</exception>
            /// <exception cref = "ArgumentException"><paramref name = "protocolName"/> is the empty string ("") or white space.</exception>
            /// <exception cref = "ArgumentException"><paramref name = "protocolVersion"/> is the empty string ("") or white space.</exception>
            protected DmsAlarmTemplate(Skyline.DataMiner.Library.Common.IDms dms, string name, string protocolName, string protocolVersion): base(dms, name, protocolName, protocolVersion)
            {
            }

            /// <summary>
            /// Loads all the data and properties found related to the alarm template.
            /// </summary>
            /// <exception cref = "TemplateNotFoundException">The template does not exist in the DataMiner system.</exception>
            internal override void Load()
            {
                Skyline.DataMiner.Net.Messages.GetAlarmTemplateMessage message = new Skyline.DataMiner.Net.Messages.GetAlarmTemplateMessage{AsOneObject = true, Protocol = Protocol.Name, Version = Protocol.Version, Template = Name};
                Skyline.DataMiner.Net.Messages.AlarmTemplateEventMessage response = (Skyline.DataMiner.Net.Messages.AlarmTemplateEventMessage)Dms.Communication.SendSingleResponseMessage(message);
                if (response != null)
                {
                    Parse(response);
                }
                else
                {
                    throw new Skyline.DataMiner.Library.Common.TemplateNotFoundException(Name, Protocol.Name, Protocol.Version);
                }
            }

            /// <summary>
            /// Parses the alarm template event message.
            /// </summary>
            /// <param name = "message">The message received from SLNet.</param>
            internal abstract void Parse(Skyline.DataMiner.Net.Messages.AlarmTemplateEventMessage message);
        }

        /// <summary>
        /// Represents an alarm template group.
        /// </summary>
        internal class DmsAlarmTemplateGroup : Skyline.DataMiner.Library.Common.Templates.DmsAlarmTemplate, Skyline.DataMiner.Library.Common.Templates.IDmsAlarmTemplateGroup
        {
            /// <summary>
            /// The entries of the alarm group.
            /// </summary>
            private readonly System.Collections.Generic.List<Skyline.DataMiner.Library.Common.Templates.IDmsAlarmTemplateGroupEntry> entries = new System.Collections.Generic.List<Skyline.DataMiner.Library.Common.Templates.IDmsAlarmTemplateGroupEntry>();
            /// <summary>
            /// Initializes a new instance of the <see cref = "DmsAlarmTemplateGroup"/> class.
            /// </summary>
            /// <param name = "dms">Object implementing the <see cref = "IDms"/> interface.</param>
            /// <param name = "name">The name of the alarm template.</param>
            /// <param name = "protocol">The protocol this alarm template group corresponds with.</param>
            /// <exception cref = "ArgumentNullException"><paramref name = "dms"/> is <see langword = "null"/>.</exception>
            /// <exception cref = "ArgumentNullException"><paramref name = "name"/> is <see langword = "null"/>.</exception>
            /// <exception cref = "ArgumentNullException"><paramref name = "protocol"/> is <see langword = "null"/>.</exception>
            /// <exception cref = "ArgumentException"><paramref name = "name"/> is the empty string ("") or white space.</exception>
            internal DmsAlarmTemplateGroup(Skyline.DataMiner.Library.Common.IDms dms, string name, Skyline.DataMiner.Library.Common.DmsProtocol protocol): base(dms, name, protocol)
            {
                IsLoaded = false;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref = "DmsAlarmTemplateGroup"/> class.
            /// </summary>
            /// <param name = "dms">Instance of <see cref = "Dms"/>.</param>
            /// <param name = "alarmTemplateEventMessage">An instance of AlarmTemplateEventMessage.</param>
            /// <exception cref = "ArgumentNullException"><paramref name = "dms"/> is <see langword = "null"/>.</exception>
            /// <exception cref = "ArgumentException"><paramref name = "alarmTemplateEventMessage"/> is invalid.</exception>
            internal DmsAlarmTemplateGroup(Skyline.DataMiner.Library.Common.IDms dms, Skyline.DataMiner.Net.Messages.AlarmTemplateEventMessage alarmTemplateEventMessage): base(dms, alarmTemplateEventMessage.Name, alarmTemplateEventMessage.Protocol, alarmTemplateEventMessage.Version)
            {
                IsLoaded = true;
                foreach (Skyline.DataMiner.Net.Messages.AlarmTemplateGroupEntry entry in alarmTemplateEventMessage.GroupEntries)
                {
                    Skyline.DataMiner.Library.Common.Templates.IDmsAlarmTemplate template = Protocol.GetAlarmTemplate(entry.Name);
                    entries.Add(new Skyline.DataMiner.Library.Common.Templates.DmsAlarmTemplateGroupEntry(template, entry.IsEnabled, entry.IsScheduled));
                }
            }

            /// <summary>
            /// Returns a string that represents the current object.
            /// </summary>
            /// <returns>A string that represents the current object.</returns>
            public override string ToString()
            {
                return System.String.Format(System.Globalization.CultureInfo.InvariantCulture, "Template Group Name: {0}, Protocol Name: {1}, Protocol Version: {2}", Name, Protocol.Name, Protocol.Version);
            }

            /// <summary>
            /// Parses the alarm template event message.
            /// </summary>
            /// <param name = "message">The message received from the SLNet process.</param>
            internal override void Parse(Skyline.DataMiner.Net.Messages.AlarmTemplateEventMessage message)
            {
                IsLoaded = true;
                entries.Clear();
                foreach (Skyline.DataMiner.Net.Messages.AlarmTemplateGroupEntry entry in message.GroupEntries)
                {
                    Skyline.DataMiner.Library.Common.Templates.IDmsAlarmTemplate template = Protocol.GetAlarmTemplate(entry.Name);
                    entries.Add(new Skyline.DataMiner.Library.Common.Templates.DmsAlarmTemplateGroupEntry(template, entry.IsEnabled, entry.IsScheduled));
                }
            }
        }

        /// <summary>
        /// Represents an alarm group entry.
        /// </summary>
        internal class DmsAlarmTemplateGroupEntry : Skyline.DataMiner.Library.Common.Templates.IDmsAlarmTemplateGroupEntry
        {
            /// <summary>
            /// The template which is an entry of the alarm group.
            /// </summary>
            private readonly Skyline.DataMiner.Library.Common.Templates.IDmsAlarmTemplate template;
            /// <summary>
            /// Specifies whether this entry is enabled.
            /// </summary>
            private readonly bool isEnabled;
            /// <summary>
            /// Specifies whether this entry is scheduled.
            /// </summary>
            private readonly bool isScheduled;
            /// <summary>
            /// Initializes a new instance of the <see cref = "DmsAlarmTemplateGroupEntry"/> class.
            /// </summary>
            /// <param name = "template">The alarm template.</param>
            /// <param name = "isEnabled">Specifies if the entry is enabled.</param>
            /// <param name = "isScheduled">Specifies if the entry is scheduled.</param>
            internal DmsAlarmTemplateGroupEntry(Skyline.DataMiner.Library.Common.Templates.IDmsAlarmTemplate template, bool isEnabled, bool isScheduled)
            {
                if (template == null)
                {
                    throw new System.ArgumentNullException("template");
                }

                this.template = template;
                this.isEnabled = isEnabled;
                this.isScheduled = isScheduled;
            }

            /// <summary>
            /// Returns a string that represents the current object.
            /// </summary>
            /// <returns>A string that represents the current object.</returns>
            public override string ToString()
            {
                return System.String.Format(System.Globalization.CultureInfo.InvariantCulture, "Alarm template group entry:{0}", template.Name);
            }
        }

        /// <summary>
        /// Represents a standalone alarm template.
        /// </summary>
        internal class DmsStandaloneAlarmTemplate : Skyline.DataMiner.Library.Common.Templates.DmsAlarmTemplate, Skyline.DataMiner.Library.Common.Templates.IDmsStandaloneAlarmTemplate
        {
            /// <summary>
            /// The description of the alarm definition.
            /// </summary>
            private string description;
            /// <summary>
            /// Indicates whether this alarm template is used in a group.
            /// </summary>
            private bool isUsedInGroup;
            /// <summary>
            /// Initializes a new instance of the <see cref = "DmsStandaloneAlarmTemplate"/> class.
            /// </summary>
            /// <param name = "dms">Object implementing the <see cref = "IDms"/> interface.</param>
            /// <param name = "name">The name of the alarm template.</param>
            /// <param name = "protocol">The protocol this standalone alarm template corresponds with.</param>
            /// <exception cref = "ArgumentNullException"><paramref name = "dms"/> is <see langword = "null"/>.</exception>
            /// <exception cref = "ArgumentNullException"><paramref name = "name"/> is <see langword = "null"/>.</exception>
            /// <exception cref = "ArgumentNullException"><paramref name = "protocol"/> is <see langword = "null"/>.</exception>
            /// <exception cref = "ArgumentException"><paramref name = "name"/> is the empty string ("") or white space.</exception>
            internal DmsStandaloneAlarmTemplate(Skyline.DataMiner.Library.Common.IDms dms, string name, Skyline.DataMiner.Library.Common.DmsProtocol protocol): base(dms, name, protocol)
            {
                IsLoaded = false;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref = "DmsStandaloneAlarmTemplate"/> class.
            /// </summary>
            /// <param name = "dms">The DataMiner system reference.</param>
            /// <param name = "alarmTemplateEventMessage">An instance of AlarmTemplateEventMessage.</param>
            /// <exception cref = "ArgumentNullException"><paramref name = "dms"/> is <see langword = "null"/>.</exception>
            /// <exception cref = "ArgumentException"><paramref name = "dms"/> is invalid.</exception>
            internal DmsStandaloneAlarmTemplate(Skyline.DataMiner.Library.Common.IDms dms, Skyline.DataMiner.Net.Messages.AlarmTemplateEventMessage alarmTemplateEventMessage): base(dms, alarmTemplateEventMessage.Name, alarmTemplateEventMessage.Protocol, alarmTemplateEventMessage.Version)
            {
                IsLoaded = true;
                description = alarmTemplateEventMessage.Description;
                isUsedInGroup = alarmTemplateEventMessage.IsUsedInGroup;
            }

            /// <summary>
            /// Returns a string that represents the current object.
            /// </summary>
            /// <returns>A string that represents the current object.</returns>
            public override string ToString()
            {
                return System.String.Format(System.Globalization.CultureInfo.InvariantCulture, "Alarm Template Name: {0}, Protocol Name: {1}, Protocol Version: {2}", Name, Protocol.Name, Protocol.Version);
            }

            /// <summary>
            /// Parses the alarm template event message.
            /// </summary>
            /// <param name = "message">The message received from SLNet.</param>
            internal override void Parse(Skyline.DataMiner.Net.Messages.AlarmTemplateEventMessage message)
            {
                IsLoaded = true;
                description = message.Description;
                isUsedInGroup = message.IsUsedInGroup;
            }
        }

        /// <summary>
        /// Represents an alarm template.
        /// </summary>
        internal abstract class DmsTemplate : Skyline.DataMiner.Library.Common.DmsObject
        {
            /// <summary>
            /// Alarm template name.
            /// </summary>
            private readonly string name;
            /// <summary>
            /// The protocol this alarm template corresponds with.
            /// </summary>
            private readonly Skyline.DataMiner.Library.Common.DmsProtocol protocol;
            /// <summary>
            /// Initializes a new instance of the <see cref = "DmsTemplate"/> class.
            /// </summary>
            /// <param name = "dms">Object implementing the <see cref = "IDms"/> interface.</param>
            /// <param name = "name">The name of the alarm template.</param>
            /// <param name = "protocol">Instance of the protocol.</param>
            /// <exception cref = "ArgumentNullException"><paramref name = "dms"/> is <see langword = "null"/>.</exception>
            /// <exception cref = "ArgumentNullException"><paramref name = "name"/> is <see langword = "null"/>.</exception>
            /// <exception cref = "ArgumentNullException"><paramref name = "protocol"/> is <see langword = "null"/>.</exception>
            /// <exception cref = "ArgumentException"><paramref name = "name"/> is the empty string ("") or white space.</exception>
            protected DmsTemplate(Skyline.DataMiner.Library.Common.IDms dms, string name, Skyline.DataMiner.Library.Common.DmsProtocol protocol): base(dms)
            {
                if (name == null)
                {
                    throw new System.ArgumentNullException("name");
                }

                if (protocol == null)
                {
                    throw new System.ArgumentNullException("protocol");
                }

                if (System.String.IsNullOrWhiteSpace(name))
                {
                    throw new System.ArgumentException("The name of the template is the empty string (\"\") or white space.");
                }

                this.name = name;
                this.protocol = protocol;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref = "DmsTemplate"/> class.
            /// </summary>
            /// <param name = "dms">The DataMiner System reference.</param>
            /// <param name = "name">The template name.</param>
            /// <param name = "protocolName">The name of the protocol.</param>
            /// <param name = "protocolVersion">The version of the protocol.</param>
            /// <exception cref = "ArgumentNullException"><paramref name = "dms"/> is <see langword = "null"/>.</exception>
            /// <exception cref = "ArgumentNullException"><paramref name = "name"/> is <see langword = "null"/>.</exception>
            /// <exception cref = "ArgumentNullException"><paramref name = "protocolName"/> is <see langword = "null"/>.</exception>
            /// <exception cref = "ArgumentNullException"><paramref name = "protocolVersion"/> is <see langword = "null"/>.</exception>
            /// <exception cref = "ArgumentException"><paramref name = "name"/> is the empty string ("") or white space.</exception>
            /// <exception cref = "ArgumentException"><paramref name = "protocolName"/> is the empty string ("") or white space.</exception>
            /// <exception cref = "ArgumentException"><paramref name = "protocolVersion"/> is the empty string ("") or white space.</exception>
            protected DmsTemplate(Skyline.DataMiner.Library.Common.IDms dms, string name, string protocolName, string protocolVersion): base(dms)
            {
                if (name == null)
                {
                    throw new System.ArgumentNullException("name");
                }

                if (protocolName == null)
                {
                    throw new System.ArgumentNullException("protocolName");
                }

                if (protocolVersion == null)
                {
                    throw new System.ArgumentNullException("protocolVersion");
                }

                if (System.String.IsNullOrWhiteSpace(name))
                {
                    throw new System.ArgumentException("The name of the template is the empty string(\"\") or white space.", "name");
                }

                if (System.String.IsNullOrWhiteSpace(protocolName))
                {
                    throw new System.ArgumentException("The name of the protocol is the empty string (\"\") or white space.", "protocolName");
                }

                if (System.String.IsNullOrWhiteSpace(protocolVersion))
                {
                    throw new System.ArgumentException("The version of the protocol is the empty string (\"\") or white space.", "protocolVersion");
                }

                this.name = name;
                protocol = new Skyline.DataMiner.Library.Common.DmsProtocol(dms, protocolName, protocolVersion);
            }

            /// <summary>
            /// Gets the template name.
            /// </summary>
            public string Name
            {
                get
                {
                    return name;
                }
            }

            /// <summary>
            /// Gets the protocol this template corresponds with.
            /// </summary>
            public Skyline.DataMiner.Library.Common.IDmsProtocol Protocol
            {
                get
                {
                    return protocol;
                }
            }
        }

        /// <summary>
        /// Represents a trend template.
        /// </summary>
        internal class DmsTrendTemplate : Skyline.DataMiner.Library.Common.Templates.DmsTemplate, Skyline.DataMiner.Library.Common.Templates.IDmsTrendTemplate
        {
            /// <summary>
            /// Initializes a new instance of the <see cref = "DmsTrendTemplate"/> class.
            /// </summary>
            /// <param name = "dms">Object implementing the <see cref = "IDms"/> interface.</param>
            /// <param name = "name">The name of the alarm template.</param>
            /// <param name = "protocol">The instance of the protocol.</param>
            /// <exception cref = "ArgumentNullException">Dms is <see langword = "null"/>.</exception>
            /// <exception cref = "ArgumentNullException">Name is <see langword = "null"/>.</exception>
            /// <exception cref = "ArgumentNullException">Protocol is <see langword = "null"/>.</exception>
            /// <exception cref = "ArgumentException"><paramref name = "name"/> is the empty string ("") or white space.</exception>
            internal DmsTrendTemplate(Skyline.DataMiner.Library.Common.IDms dms, string name, Skyline.DataMiner.Library.Common.DmsProtocol protocol): base(dms, name, protocol)
            {
                IsLoaded = true;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref = "DmsTrendTemplate"/> class.
            /// </summary>
            /// <param name = "dms">Object implementing the <see cref = "IDms"/> interface.</param>
            /// <param name = "templateInfo">The template info received by SLNet.</param>
            /// <exception cref = "ArgumentNullException"><paramref name = "dms"/> is <see langword = "null"/>.</exception>
            /// <exception cref = "ArgumentNullException">name is <see langword = "null"/>.</exception>
            /// <exception cref = "ArgumentNullException">protocolName is <see langword = "null"/>.</exception>
            /// <exception cref = "ArgumentNullException">protocolVersion is <see langword = "null"/>.</exception>
            /// <exception cref = "ArgumentException">name is the empty string ("") or white space.</exception>
            /// <exception cref = "ArgumentException">ProtocolName is the empty string ("") or white space.</exception>
            /// <exception cref = "ArgumentException">ProtocolVersion is the empty string ("") or white space.</exception>
            internal DmsTrendTemplate(Skyline.DataMiner.Library.Common.IDms dms, Skyline.DataMiner.Net.Messages.GetTrendingTemplateInfoResponseMessage templateInfo): base(dms, templateInfo.Name, templateInfo.Protocol, templateInfo.Version)
            {
                IsLoaded = true;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref = "DmsTrendTemplate"/> class.
            /// </summary>
            /// <param name = "dms">Object implementing the <see cref = "IDms"/> interface.</param>
            /// <param name = "templateInfo">The template info received by SLNet.</param>
            /// <exception cref = "ArgumentNullException"><paramref name = "dms"/> is <see langword = "null"/>.</exception>
            /// <exception cref = "ArgumentNullException">Name is <see langword = "null"/>.</exception>
            /// <exception cref = "ArgumentNullException">ProtocolName is <see langword = "null"/>.</exception>
            /// <exception cref = "ArgumentNullException">ProtocolVersion is <see langword = "null"/>.</exception>
            /// <exception cref = "ArgumentException">Name is the empty string ("") or white space.</exception>
            /// <exception cref = "ArgumentException">ProtocolName is the empty string ("") or white space.</exception>
            /// <exception cref = "ArgumentException">ProtocolVersion is the empty string ("") or white space.</exception>
            internal DmsTrendTemplate(Skyline.DataMiner.Library.Common.IDms dms, Skyline.DataMiner.Net.Messages.TrendTemplateMetaInfo templateInfo): base(dms, templateInfo.Name, templateInfo.ProtocolName, templateInfo.ProtocolVersion)
            {
                IsLoaded = true;
            }

            /// <summary>
            /// Returns a string that represents the current object.
            /// </summary>
            /// <returns>A string that represents the current object.</returns>
            public override string ToString()
            {
                return System.String.Format(System.Globalization.CultureInfo.InvariantCulture, "Trend Template Name: {0}, Protocol Name: {1}, Protocol Version: {2}", Name, Protocol.Name, Protocol.Version);
            }

            /// <summary>
            /// Loads this object.
            /// </summary>
            internal override void Load()
            {
            }
        }

        /// <summary>
        /// DataMiner alarm template interface.
        /// </summary>
        public interface IDmsAlarmTemplate : Skyline.DataMiner.Library.Common.Templates.IDmsTemplate
        {
        }

        /// <summary>
        /// DataMiner alarm template group interface.
        /// </summary>
        public interface IDmsAlarmTemplateGroup : Skyline.DataMiner.Library.Common.Templates.IDmsAlarmTemplate
        {
        }

        /// <summary>
        /// DataMiner alarm template group entry interface.
        /// </summary>
        public interface IDmsAlarmTemplateGroupEntry
        {
        }

        /// <summary>
        /// DataMiner standalone alarm template interface.
        /// </summary>
        public interface IDmsStandaloneAlarmTemplate : Skyline.DataMiner.Library.Common.Templates.IDmsAlarmTemplate
        {
        }

        /// <summary>
        /// DataMiner template interface.
        /// </summary>
        public interface IDmsTemplate : Skyline.DataMiner.Library.Common.IDmsObject
        {
            /// <summary>
            /// Gets the template name.
            /// </summary>
            string Name
            {
                get;
            }

            /// <summary>
            /// Gets the protocol this template corresponds with.
            /// </summary>
            Skyline.DataMiner.Library.Common.IDmsProtocol Protocol
            {
                get;
            }
        }

        /// <summary>
        /// DataMiner trend template interface.
        /// </summary>
        public interface IDmsTrendTemplate : Skyline.DataMiner.Library.Common.Templates.IDmsTemplate
        {
        }
    }

    /// <summary>
    /// Base class for parameters.
    /// </summary>
    /// <typeparam name = "T">The parameter type.</typeparam>
    internal class DmsParameter<T>
    {
        /// <summary>
        /// Setter delegates.
        /// </summary>
        private static readonly System.Collections.Generic.Dictionary<System.Type, System.Func<Skyline.DataMiner.Net.Messages.SetParameterMessage, T, System.Boolean>> Setters = new System.Collections.Generic.Dictionary<System.Type, System.Func<Skyline.DataMiner.Net.Messages.SetParameterMessage, T, System.Boolean>>{{typeof(string), new System.Func<Skyline.DataMiner.Net.Messages.SetParameterMessage, T, System.Boolean>(AddStringValueToSetParameterMessage)}, {typeof(int? ), new System.Func<Skyline.DataMiner.Net.Messages.SetParameterMessage, T, System.Boolean>(AddNullableIntValueToSetParameterMessage)}, {typeof(double? ), new System.Func<Skyline.DataMiner.Net.Messages.SetParameterMessage, T, System.Boolean>(AddNullableDoubleValueToSetParameterMessage)}, {typeof(System.DateTime? ), new System.Func<Skyline.DataMiner.Net.Messages.SetParameterMessage, T, System.Boolean>(AddNullableDateTimeValueToSetParameterMessage)}};
        /// <summary>
        /// The parameter ID.
        /// </summary>
        private readonly int id;
        /// <summary>
        /// The type of the parameter.
        /// </summary>
        /// <remarks>Currently supported types: int?, double?, string, DateTime?.</remarks>
        private readonly System.Type type;
        /// <summary>
        /// The underlying type (in case of Nullable&lt;T&gt;).
        /// </summary>
        private readonly System.Type underlyingType;
        /// <summary>
        /// Initializes a new instance of the <see cref = "DmsParameter{T}"/> class.
        /// </summary>
        /// <param name = "id">The parameter ID.</param>
        /// <exception cref = "ArgumentException"><paramref name = "id"/> is invalid.</exception>
        protected DmsParameter(int id)
        {
            if (id < 0)
            {
                throw new System.ArgumentException("Invalid parameter ID", "id");
            }

            this.id = id;
            type = typeof(T);
            underlyingType = System.Nullable.GetUnderlyingType(type);
        }

        /// <summary>
        /// Gets the parameter ID.
        /// </summary>
        /// <value>The parameter ID.</value>
        public int Id
        {
            get
            {
                return id;
            }
        }

        /// <summary>
        /// Adds the value to set to the SetParameterMessage.
        /// </summary>
        /// <param name = "message">The message to update with the parameter value to set.</param>
        /// <param name = "value">The parameter value to set.</param>
        /// <returns>Whether the SetParameterMessage needs to be sent.</returns>
        protected bool AddValueToSetParameterMessage(Skyline.DataMiner.Net.Messages.SetParameterMessage message, T value)
        {
            System.Func<Skyline.DataMiner.Net.Messages.SetParameterMessage, T, System.Boolean> setter;
            if (Setters.TryGetValue(type, out setter))
            {
                return setter(message, value);
            }
            else
            {
                throw new System.NotSupportedException("Type " + typeof(T) + " is not supported.");
            }
        }

        /// <summary>
        /// Adds a nullable DateTime value to the message.
        /// </summary>
        /// <param name = "message">The message.</param>
        /// <param name = "value">The value.</param>
        /// <returns><c>true</c> if the set message needs to be executed; otherwise, <c>false</c>.</returns>
        private static bool AddNullableDateTimeValueToSetParameterMessage(Skyline.DataMiner.Net.Messages.SetParameterMessage message, T value)
        {
            bool executeSet = true;
            if (!value.Equals(default(T)))
            {
                System.DateTime valueToSet = (System.DateTime)System.Convert.ChangeType(value, typeof(System.DateTime), System.Globalization.CultureInfo.CurrentCulture);
                message.Value = new Skyline.DataMiner.Net.Messages.ParameterValue(valueToSet);
            }
            else
            {
                executeSet = false;
            }

            return executeSet;
        }

        /// <summary>
        /// Adds a nullable double value to the message.
        /// </summary>
        /// <param name = "message">The message.</param>
        /// <param name = "value">The value.</param>
        /// <returns><c>true</c> if the set message needs to be executed; otherwise, <c>false</c>.</returns>
        private static bool AddNullableDoubleValueToSetParameterMessage(Skyline.DataMiner.Net.Messages.SetParameterMessage message, T value)
        {
            bool executeSet = true;
            if (!value.Equals(default(T)))
            {
                double valueToSet = (double)System.Convert.ChangeType(value, typeof(double), System.Globalization.CultureInfo.CurrentCulture);
                message.Value = new Skyline.DataMiner.Net.Messages.ParameterValue(valueToSet);
            }
            else
            {
                executeSet = false;
            }

            return executeSet;
        }

        /// <summary>
        /// Adds a nullable int value to the message.
        /// </summary>
        /// <param name = "message">The message.</param>
        /// <param name = "value">The string value.</param>
        /// <returns><c>true</c> if the set message needs to be executed; otherwise, <c>false</c>.</returns>
        private static bool AddNullableIntValueToSetParameterMessage(Skyline.DataMiner.Net.Messages.SetParameterMessage message, T value)
        {
            bool executeSet = true;
            if (!value.Equals(default(T)))
            {
                int valueToSet = (int)System.Convert.ChangeType(value, typeof(int), System.Globalization.CultureInfo.CurrentCulture);
                message.Value = new Skyline.DataMiner.Net.Messages.ParameterValue(valueToSet);
            }
            else
            {
                executeSet = false;
            }

            return executeSet;
        }

        /// <summary>
        /// Adds a string value to the message.
        /// </summary>
        /// <param name = "message">The message.</param>
        /// <param name = "value">The string value.</param>
        /// <returns><c>true</c> if the set message needs to be executed; otherwise, <c>false</c>.</returns>
        private static bool AddStringValueToSetParameterMessage(Skyline.DataMiner.Net.Messages.SetParameterMessage message, T value)
        {
            message.Value = new Skyline.DataMiner.Net.Messages.ParameterValue((string)System.Convert.ChangeType(value, typeof(string), System.Globalization.CultureInfo.CurrentCulture));
            return true;
        }
    }

    /// <summary>
    /// Represents a standalone parameter.
    /// </summary>
    /// <typeparam name = "T">The type of the standalone parameter.</typeparam>
    /// <remarks>
    /// In case T equals int?, double? or DateTime?, extension methods are available. Refer to <see 
    ///cref = "ExtensionsIDmsStandaloneParameter"/> for more information.
    /// </remarks>
    internal class DmsStandaloneParameter<T> : Skyline.DataMiner.Library.Common.DmsParameter<T>, Skyline.DataMiner.Library.Common.IDmsStandaloneParameter<T>
    {
        /// <summary>
        /// The element this parameter is part of.
        /// </summary>
        private readonly Skyline.DataMiner.Library.Common.IDmsElement element;
        /// <summary>
        /// Initializes a new instance of the <see cref = "DmsStandaloneParameter{T}"/> class.
        /// </summary>
        /// <param name = "element">The element that the parameter belongs to.</param>
        /// <param name = "id">The ID of the parameter.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "element"/> is <see langword = "null"/>.</exception>
        /// <exception cref = "ArgumentException"><paramref name = "id"/> is invalid.</exception>
        internal DmsStandaloneParameter(Skyline.DataMiner.Library.Common.IDmsElement element, int id): base(id)
        {
            if (element == null)
            {
                throw new System.ArgumentNullException("element");
            }

            this.element = element;
        }

        /// <summary>
        /// Sets the value of this parameter.
        /// </summary>
        /// <param name = "value">The value to set.</param>
        /// <exception cref = "ElementStoppedException">The element is stopped.</exception>
        /// <exception cref = "ElementNotFoundException">
        /// The element was not found in the DataMiner System.
        /// </exception>
        public void SetValue(T value)
        {
            Skyline.DataMiner.Library.Common.HelperClass.CheckElementState(element);
            Skyline.DataMiner.Net.Messages.SetParameterMessage message = new Skyline.DataMiner.Net.Messages.SetParameterMessage{DataMinerID = element.DmsElementId.AgentId, ElId = element.DmsElementId.ElementId, ParameterId = Id, DisableInformationEventMessage = true};
            if (AddValueToSetParameterMessage(message, value))
            {
                element.Host.Dms.Communication.SendMessage(message);
            }
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return System.String.Format(System.Globalization.CultureInfo.InvariantCulture, "Standalone Parameter:{0}", Id);
        }
    }

    /// <summary>
    /// DataMiner standalone parameter interface.
    /// </summary>
    public interface IDmsStandaloneParameter
    {
        /// <summary>
        /// Gets the ID of this parameter.
        /// </summary>
        /// <value>The ID of this parameter.</value>
        int Id
        {
            get;
        }
    }

    /// <summary>
    /// DataMiner standalone parameter interface for a parameter of a specific type.
    /// </summary>
    /// <typeparam name = "T">The type of the standalone parameter.</typeparam>
    public interface IDmsStandaloneParameter<T> : Skyline.DataMiner.Library.Common.IDmsStandaloneParameter
    {
        /// <summary>
        /// Sets the value of this parameter.
        /// </summary>
        /// <param name = "value">The value to set.</param>
        /// <exception cref = "ElementStoppedException">The element is stopped.</exception>
        /// <exception cref = "ElementNotFoundException">The element was not found in the DataMiner System.</exception>
        void SetValue(T value);
    }

    /// <summary>
    /// Defines extension methods on the <see cref = "IConnection"/> class.
    /// </summary>
    ///
    [Skyline.DataMiner.Library.Common.Attributes.DllImport("SLNetTypes.dll")]
    public static class ConnectionInterfaceExtensions
    {
        /// <summary>
        /// Gets an object implementing the <see cref = "IDms"/> interface using an object that implements the  <see cref = "IConnection"/> class.
        /// </summary>
        /// <param name = "connection">The connection interface.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "connection"/> is <see langword = "null"/>.</exception>
        /// <returns>Object implementing the <see cref = "IDms"/> interface.</returns>
        public static Skyline.DataMiner.Library.Common.IDms GetDms(this Skyline.DataMiner.Net.IConnection connection)
        {
            if (connection == null)
            {
                throw new System.ArgumentNullException("connection");
            }

            return new Skyline.DataMiner.Library.Common.Dms(new Skyline.DataMiner.Library.Common.ConnectionCommunication(connection));
        }
    }

    namespace InterAppCalls
    {
        namespace CallBulk
        {
            /// <summary>
            /// Represents an inter-app call containing multiple messages.
            /// </summary>
            public interface IInterAppCall
            {
                /// <summary>
                /// Gets or sets a globally unique identifier (GUID) identifying this inter-app call.
                /// </summary>
                /// <value>A globally unique identifier (GUID) identifying this inter-app call.</value>
                string Guid
                {
                    get;
                    set;
                }

                /// <summary>
                /// The serializer used internally to convert data to and from a class model.
                /// </summary>
                Skyline.DataMiner.Library.Common.Serializing.ISerializer InternalSerializer
                {
                    get;
                    set;
                }

                /// <summary>
                /// Gets or sets all messages of this inter-app call.
                /// </summary>
                /// <value>The messages of this inter-app call.</value>
                Skyline.DataMiner.Library.Common.InterAppCalls.CallBulk.Messages Messages
                {
                    get;
                }

                /// <summary>
                /// Gets or sets the time this call was received.
                /// </summary>
                /// <value>The time this call was received.</value>
                System.DateTime ReceivingTime
                {
                    get;
                    set;
                }

                /// <summary>
                /// Gets the time this call was sent.
                /// </summary>
                /// <value>The time this call was sent.</value>
                System.DateTime SendingTime
                {
                    get;
                }
            }

            [Skyline.DataMiner.Library.Common.Attributes.DllImport("System.Runtime.Serialization.dll")]
            internal class InterAppCall : Skyline.DataMiner.Library.Common.InterAppCalls.CallBulk.IInterAppCall
            {
                private Skyline.DataMiner.Library.Common.Serializing.ISerializer internalSerializer;
                public InterAppCall(string guid)
                {
                    if (System.String.IsNullOrWhiteSpace(guid))
                    {
                        throw new System.ArgumentNullException("guid", "Identifier should not be empty or null.");
                    }

                    Guid = guid;
                    Messages = new Skyline.DataMiner.Library.Common.InterAppCalls.CallBulk.Messages(this);
                }

                public InterAppCall()
                {
                    Guid = System.Guid.NewGuid().ToString();
                    Messages = new Skyline.DataMiner.Library.Common.InterAppCalls.CallBulk.Messages(this);
                }

                public string Guid
                {
                    get;
                    set;
                }

                /// <summary>
                /// The internal serializer used to serialize this message.
                /// </summary>
                [System.Runtime.Serialization.IgnoreDataMember]
                public Skyline.DataMiner.Library.Common.Serializing.ISerializer InternalSerializer
                {
                    get
                    {
                        if (internalSerializer == null)
                        {
                            internalSerializer = Skyline.DataMiner.Library.Common.Serializing.SerializerFactory.CreateInterAppSerializer(typeof(Skyline.DataMiner.Library.Common.InterAppCalls.CallBulk.InterAppCall));
                        }

                        return internalSerializer;
                    }

                    set
                    {
                        internalSerializer = value;
                    }
                }

                public Skyline.DataMiner.Library.Common.InterAppCalls.CallBulk.Messages Messages
                {
                    get;
                    private set;
                }

                public System.DateTime ReceivingTime
                {
                    get;
                    set;
                }

                public System.DateTime SendingTime
                {
                    get;
                    private set;
                }
            }

            /// <summary>
            /// Factory class that can create inter-app calls.
            /// </summary>
            public static class InterAppCallFactory
            {
                /// <summary>
                /// Creates an inter-app call from the specified string.
                /// </summary>
                /// <param name = "rawData">The serialized raw data.</param>
                /// <returns>An inter-app call.</returns>
                /// <param name = "serializer">Optional serializer to use. Leave empty to use default.</param>
                /// <exception cref = "ArgumentNullException"><paramref name = "rawData"/> is empty or null.</exception>
                /// <exception cref = "ArgumentException">Format of <paramref name = "rawData"/> is invalid and deserialization failed.</exception>
                public static Skyline.DataMiner.Library.Common.InterAppCalls.CallBulk.IInterAppCall CreateFromRaw(string rawData, Skyline.DataMiner.Library.Common.Serializing.ISerializer serializer = null)
                {
                    if (System.String.IsNullOrWhiteSpace(rawData))
                        throw new System.ArgumentNullException("rawData");
                    if (serializer == null)
                        serializer = Skyline.DataMiner.Library.Common.Serializing.SerializerFactory.CreateInterAppSerializer(typeof(Skyline.DataMiner.Library.Common.InterAppCalls.CallBulk.InterAppCall));
                    var returnedResult = serializer.DeserializeFromString<Skyline.DataMiner.Library.Common.InterAppCalls.CallBulk.InterAppCall>(rawData);
                    returnedResult.ReceivingTime = System.DateTime.Now;
                    returnedResult.InternalSerializer = serializer;
                    return returnedResult;
                }
            }

            /// <summary>
            /// Represents a collection of messages.
            /// </summary>
            public class Messages : System.Collections.Generic.ICollection<Skyline.DataMiner.Library.Common.InterAppCalls.CallSingle.Message>
            {
                private readonly System.Collections.Generic.Dictionary<System.String, Skyline.DataMiner.Library.Common.InterAppCalls.CallSingle.Message> content = new System.Collections.Generic.Dictionary<System.String, Skyline.DataMiner.Library.Common.InterAppCalls.CallSingle.Message>();
                /// <summary>
                /// Initializes a new instance of the <see cref = "Messages"/> class.
                /// </summary>
                public Messages()
                {
                }

                /// <summary>
                /// Initializes a new instance of the <see cref = "Messages"/> class using the specified parent inter-app call.
                /// </summary>
                /// <param name = "parentCall">The parent inter-app call.</param>
                public Messages(Skyline.DataMiner.Library.Common.InterAppCalls.CallBulk.IInterAppCall parentCall)
                {
                    ParentCall = parentCall;
                }

                /// <summary>
                /// Gets the number of messages contained in this object.
                /// </summary>
                /// <value>The number of messages contained in this object.</value>
                public int Count
                {
                    get
                    {
                        return content.Count;
                    }
                }

                /// <summary>
                /// Gets a value indicating whether this object is read-only.
                /// </summary>
                /// <value><c>true</c> if read-only; otherwise, <c>false</c>.</value>
                public bool IsReadOnly
                {
                    get
                    {
                        return false;
                    }
                }

                /// <summary>
                /// Gets or sets the parent inter-app call.
                /// </summary>
                /// <value>The parent inter-app call or <see langword = "null"/> if there is no parent.</value>
                public Skyline.DataMiner.Library.Common.InterAppCalls.CallBulk.IInterAppCall ParentCall
                {
                    get;
                    set;
                }

                /// <summary>
                /// Adds a new message.
                /// </summary>
                /// <param name = "item">The message to add.</param>
                /// <exception cref = "ArgumentNullException"><paramref name = "item"/> is <see langword = "null"/>.</exception>
                /// <exception cref = "ArgumentException">The provided message should not implement the <see cref = "MessageExecution.IMessageExecutor"/> interface.
                /// -or-
                /// An item with the same GUID was already added.
                /// -or-
                /// The GUID of the item is <see langword = "null"/>.
                /// </exception>
                public void Add(Skyline.DataMiner.Library.Common.InterAppCalls.CallSingle.Message item)
                {
                    AddMessage(item);
                }

                /// <summary>
                /// Adds the specified messages to the collection.
                /// </summary>
                /// <param name = "messages">The messages to add.</param>
                /// <exception cref = "ArgumentNullException"><paramref name = "messages"/> is <see langword = "null"/>.</exception>
                /// <exception cref = "ArgumentException">The provided message should not implement the <see cref = "MessageExecution.IMessageExecutor"/> interface.
                /// -or-
                /// An item with the same GUID was already added.
                /// -or-
                /// A message or the GUID of the message is <see langword = "null"/>.
                /// </exception>
                public void AddMessage(params Skyline.DataMiner.Library.Common.InterAppCalls.CallSingle.Message[] messages)
                {
                    if (messages == null)
                    {
                        throw new System.ArgumentNullException("messages");
                    }

                    System.Type executorType = typeof(Skyline.DataMiner.Library.Common.InterAppCalls.MessageExecution.IMessageExecutor);
                    for (int i = 0; i < messages.Length; i++)
                    {
                        if (executorType.IsAssignableFrom(messages[i].GetType()))
                        {
                            throw new System.ArgumentException("Message provided should not implement IMessageExecutor. Make sure you decouple data and logic.", "messages");
                        }

                        if (messages[i] == null || messages[i].Guid == null)
                        {
                            throw new System.ArgumentException("message or message GUID must not be null");
                        }

                        content.Add(messages[i].Guid, messages[i]);
                    }
                }

                /// <summary>
                /// Clears the current collection.
                /// </summary>
                public void Clear()
                {
                    content.Clear();
                }

                /// <summary>
                /// Checks if the collection contains the specified message.
                /// </summary>
                /// <param name = "item">The item to check.</param>
                /// <exception cref = "ArgumentNullException"><paramref name = "item"/> is <see langword = "null"/>.</exception>
                /// <exception cref = "ArgumentException">The <see cref = "Message.Guid"/> property of <paramref name = "item"/> is <see langword = "null"/>.</exception>
                /// <returns><c>true</c> if <paramref name = "item"/> is present in the collection; otherwise, <c>false</c>.</returns>
                public bool Contains(Skyline.DataMiner.Library.Common.InterAppCalls.CallSingle.Message item)
                {
                    if (item == null)
                    {
                        throw new System.ArgumentNullException("item");
                    }

                    if (item.Guid == null)
                    {
                        throw new System.ArgumentException("the GUID of the specified item must not be null.");
                    }

                    return content.ContainsKey(item.Guid);
                }

                /// <summary>
                /// Copies the full content of this collection to a different one.
                /// </summary>
                /// <param name = "array">The target collection to copy into.</param>
                /// <param name = "arrayIndex">The index to start from on the destination collection.</param>
                public void CopyTo(Skyline.DataMiner.Library.Common.InterAppCalls.CallSingle.Message[] array, int arrayIndex)
                {
                    var valuesArray = System.Linq.Enumerable.ToArray(content.Values);
                    System.Array.Copy(valuesArray, 0, array, arrayIndex, valuesArray.Length);
                }

                /// <summary>
                /// Returns an enumerator that iterates through the collection.
                /// </summary>
                /// <returns>An enumerator that can be used to iterate through the collection.</returns>
                public System.Collections.Generic.IEnumerator<Skyline.DataMiner.Library.Common.InterAppCalls.CallSingle.Message> GetEnumerator()
                {
                    return content.Values.GetEnumerator();
                }

                /// <summary>
                /// Returns an enumerator that iterates through a collection.
                /// </summary>
                /// <returns>An <see cref = "IEnumerator"/> object that can be used to iterate through the collection.</returns>
                System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
                {
                    return this.GetEnumerator();
                }

                /// <summary>
                /// Removes a message from this collection.
                /// </summary>
                /// <param name = "item">The message item you want to remove.</param>
                /// <exception cref = "ArgumentNullException"><paramref name = "item"/> is <see langword = "null"/>.</exception>
                /// <exception cref = "ArgumentException">The <see cref = "Message.Guid"/> property of <paramref name = "item"/> is <see langword = "null"/>.</exception>
                /// <returns>A boolean indicating if the removal was successful.</returns>
                public bool Remove(Skyline.DataMiner.Library.Common.InterAppCalls.CallSingle.Message item)
                {
                    if (item == null)
                    {
                        throw new System.ArgumentNullException("item");
                    }

                    if (item.Guid == null)
                    {
                        throw new System.ArgumentException("the GUID of the specified item must not be null.");
                    }

                    return RemoveMessage(item.Guid);
                }

                /// <summary>
                /// Removes one or more messages from this collection by using the GUIDs.
                /// </summary>
                /// <param name = "guids">The global unique identifiers for messages you want to remove.</param>
                /// <exception cref = "ArgumentNullException"><paramref name = "guids"/> is <see langword = "null"/>.</exception>
                /// <exception cref = "ArgumentException">A GUID must not be <see langword = "null"/>.</exception>
                /// <returns>A boolean indicating if the removal was successful.</returns>
                public bool RemoveMessage(params string[] guids)
                {
                    if (guids == null)
                    {
                        throw new System.ArgumentNullException("guids");
                    }

                    bool result = true;
                    foreach (var guid in guids)
                    {
                        if (guid == null)
                        {
                            throw new System.ArgumentException("guid must not be null");
                        }

                        bool innerResult = content.Remove(guid);
                        result = result && innerResult;
                    }

                    return result;
                }
            }
        }

        namespace CallSingle
        {
            /// <summary>
            /// Represents a single command or response.
            /// </summary>
            [Skyline.DataMiner.Library.Common.Attributes.DllImport("System.Runtime.Serialization.dll")]
            public class Message
            {
                private Skyline.DataMiner.Library.Common.Serializing.ISerializer internalSerializer;
                /// <summary>
                /// Initializes a new instance of the <see cref = "Message"/> class.
                /// </summary>
                /// <remarks>Creates an instance of Message with a new GUID created using <see cref = "System.Guid.NewGuid"/>.</remarks>
                public Message()
                {
                    Guid = System.Guid.NewGuid().ToString();
                }

                /// <summary>
                /// Initializes a new instance of the <see cref = "Message"/> class using the specified GUID.
                /// </summary>
                /// <param name = "guid">The GUID.</param>
                public Message(string guid)
                {
                    Guid = guid;
                }

                /// <summary>
                /// Gets or sets a globally unique identifier (GUID) identifying this message.
                /// </summary>
                /// <value>A globally unique identifier (GUID) identifying this message.</value>
                public string Guid
                {
                    get;
                    set;
                }

                /// <summary>
                /// The internal serializer used to serialize this message.
                /// </summary>
                [System.Runtime.Serialization.IgnoreDataMember]
                public Skyline.DataMiner.Library.Common.Serializing.ISerializer InternalSerializer
                {
                    get
                    {
                        if (internalSerializer == null)
                        {
                            internalSerializer = Skyline.DataMiner.Library.Common.Serializing.SerializerFactory.CreateInterAppSerializer(typeof(Skyline.DataMiner.Library.Common.InterAppCalls.CallSingle.Message));
                        }

                        return internalSerializer;
                    }

                    set
                    {
                        internalSerializer = value;
                    }
                }

                /// <summary>
                /// Gets or sets the return address.
                /// </summary>
                /// <value>The return address.</value>
                /// <remarks>The return address represents a parameter on a specific element (identified by a DataMiner Agent ID, element ID and parameter ID) which will be checked for a return message. This use of this property is optional.</remarks>
                public Skyline.DataMiner.Library.Common.InterAppCalls.Shared.ReturnAddress ReturnAddress
                {
                    get;
                    set;
                }

                /// <summary>
                /// Gets or sets the source of this message.
                /// </summary>
                /// <value>The source of this message.</value>
                public Skyline.DataMiner.Library.Common.InterAppCalls.Shared.Source Source
                {
                    get;
                    set;
                }

                /// <summary>
                /// Retrieves the executor of this message using reflection. This contains all logic to perform when receiving this message.
                /// </summary>
                /// <returns>The executor holding all logic for processing this message.</returns>
                /// <exception cref = "AmbiguousMatchException">Unable to find executor for this type of message.</exception>
                public Skyline.DataMiner.Library.Common.InterAppCalls.MessageExecution.IMessageExecutor CreateExecutor()
                {
                    return Skyline.DataMiner.Library.Common.InterAppCalls.MessageExecution.MessageExecutorFactory.CreateExecutor(this);
                }

                /// <summary>
                /// Tries to get the Executor and run the following methods in order:
                /// DataGets (always), Parse (always), Validate (always), Modify (if validate true), DataSets (if validate true), CreateReturnMessage (always).
                /// </summary>
                /// <param name = "dataSource">A source used during DataGets, usually SLProtocol or Engine.</param>
                /// <param name = "dataDestination">A destination used during DataSets, usually SLProtocol or Engine.</param>
                /// <param name = "optionalReturnMessage">The return message that might get created in the CreateReturnMessage method.</param>
                /// <returns>A boolean to indicate if the execution was successful.</returns>
                /// <exception cref = "AmbiguousMatchException">Unable to find executor for this type of message.</exception>
                public bool TryExecute(object dataSource, object dataDestination, out Skyline.DataMiner.Library.Common.InterAppCalls.CallSingle.Message optionalReturnMessage)
                {
                    var executor = CreateExecutor();
                    return DefaultExecuteFlow(dataSource, dataDestination, out optionalReturnMessage, executor);
                }

                /// <summary>
                /// Sends this message using a default serializer and SLNet communication to a specific DataMiner Agent ID/element ID/parameter ID without waiting on a reply.
                /// </summary>
                /// <param name = "connection">The SLNet connection to use.</param>
                /// <param name = "agentId">The DataMiner Agent ID of the destination.</param>
                /// <param name = "elementId">The element ID of the destination.</param>
                /// <param name = "parameterId">The parameter ID of the destination.</param>
                public void Send(Skyline.DataMiner.Net.IConnection connection, int agentId, int elementId, int parameterId)
                {
                    var destination = new Skyline.DataMiner.Library.Common.DmsElementId(agentId, elementId);
                    Skyline.DataMiner.Library.Common.IDms thisDma = Skyline.DataMiner.Library.Common.ConnectionInterfaceExtensions.GetDms(connection);
                    var element = thisDma.GetElement(destination);
                    var parameter = element.GetStandaloneParameter<string>(parameterId);
                    System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                    sw.Start();
                    string value = Serialize();
                    System.Diagnostics.Debug.WriteLine("CLP - InterApp - Serialized: " + sw.ElapsedMilliseconds + " ms");
                    sw.Restart();
                    parameter.SetValue(value);
                    System.Diagnostics.Debug.WriteLine("CLP - InterApp - Value Set to external pid: " + sw.ElapsedMilliseconds + " ms");
                }

                /// <summary>
                /// Serializes this object using the internal ISerializer.
                /// </summary>
                /// <returns>The serialized string of this object.</returns>
                public string Serialize()
                {
                    return InternalSerializer.SerializeToString(this);
                }

                private static bool DefaultExecuteFlow(object dataSource, object dataDestination, out Skyline.DataMiner.Library.Common.InterAppCalls.CallSingle.Message optionalReturnMessage, Skyline.DataMiner.Library.Common.InterAppCalls.MessageExecution.IMessageExecutor executor)
                {
                    executor.DataGets(dataSource);
                    executor.Parse();
                    bool result = executor.Validate();
                    if (result)
                    {
                        executor.Modify();
                        executor.DataSets(dataDestination);
                    }

                    optionalReturnMessage = executor.CreateReturnMessage();
                    return result;
                }
            }

            /// <summary>
            /// A static factory to create a single <see cref = "Message"/>.
            /// </summary>
            public static class MessageFactory
            {
                /// <summary>
                /// Creates a message from a raw serialized string.
                /// </summary>
                /// <param name = "rawData">The serialized raw data.</param>
                /// <param name = "serializer">Optional serializer to use. Leave empty to use default.</param>
                /// <returns>The message.</returns>
                /// <exception cref = "ArgumentException">Format of <paramref name = "rawData"/> is invalid and deserialization failed.</exception>
                /// <exception cref = "ArgumentNullException"><paramref name = "rawData"/> was <see langword = "null"/> or empty.</exception>
                public static Skyline.DataMiner.Library.Common.InterAppCalls.CallSingle.Message CreateFromRaw(string rawData, Skyline.DataMiner.Library.Common.Serializing.ISerializer serializer = null)
                {
                    if (System.String.IsNullOrWhiteSpace(rawData))
                        throw new System.ArgumentNullException("rawData");
                    if (serializer == null)
                        serializer = Skyline.DataMiner.Library.Common.Serializing.SerializerFactory.CreateInterAppSerializer(typeof(Skyline.DataMiner.Library.Common.InterAppCalls.CallSingle.Message));
                    var returnedResult = serializer.DeserializeFromString<Skyline.DataMiner.Library.Common.InterAppCalls.CallSingle.Message>(rawData);
                    return returnedResult;
                }
            }
        }

        namespace MessageExecution
        {
            /// <summary>
            /// Represents an executor for messages. Command pattern: splits the logic into well defined methods but leaves internal logic for the concrete classes.
            /// </summary>
            public interface IMessageExecutor
            {
                /// <summary>
                /// Creates a return message. (Optional.)
                /// </summary>
                /// <returns>A message representing the response for the processed message.</returns>
                Skyline.DataMiner.Library.Common.InterAppCalls.CallSingle.Message CreateReturnMessage();
                /// <summary>
                /// Reads data from SLProtocol, Engine or other data sources. (Optional.)
                /// </summary>
                /// <param name = "dataSource">SLProtocol, Engine, or other data sources.</param>
                void DataGets(object dataSource);
                /// <summary>
                /// Writes data to SLProtocol, Engine, or another data destination. (Optional.)
                /// </summary>
                /// <param name = "dataDestination">SLProtocol, Engine, or another data destination.</param>
                void DataSets(object dataDestination);
                /// <summary>
                /// Modifies retrieved data and message data into a correct format for setting. (Optional.)
                /// </summary>
                void Modify();
                /// <summary>
                /// Parses the data retrieved from a data source in DataGets. (Optional.)
                /// </summary>
                void Parse();
                /// <summary>
                /// Validates received data for validity before attempting parsing, modification and setting. Should return true if not used.
                /// </summary>
                /// <returns>A boolean indicating if the received data is valid.</returns>
                bool Validate();
            }

            /// <summary>
            /// Represents a message executor for a specific provided message. There may only be one executor per message type.
            /// </summary>
            /// <typeparam name = "T">The message type.</typeparam>
            public abstract class MessageExecutor<T> : Skyline.DataMiner.Library.Common.InterAppCalls.MessageExecution.IMessageExecutor
            {
                /// <summary>
                /// Initializes a new instance of the <see cref = "MessageExecutor{T}"/> class using the specified message.
                /// </summary>
                /// <param name = "message">The message</param>
                protected MessageExecutor(T message)
                {
                    Message = message;
                }

                /// <summary>
                /// Gets the message to execute.
                /// </summary>
                public T Message
                {
                    get;
                    private set;
                }

                /// <summary>
                /// Creates a return message. (Optional.)
                /// </summary>
                /// <returns>A message representing the response for the processed message.</returns>
                public abstract Skyline.DataMiner.Library.Common.InterAppCalls.CallSingle.Message CreateReturnMessage();
                /// <summary>
                /// Reads data from SLProtocol, Engine or other data sources. (Optional.)
                /// </summary>
                /// <param name = "dataSource">SLProtocol, Engine, or other data sources.</param>
                public abstract void DataGets(object dataSource);
                /// <summary>
                /// Writes data to SLProtocol, Engine, or another data destination. (Optional.)
                /// </summary>
                /// <param name = "dataDestination">SLProtocol, Engine, or another data destination.</param>
                public abstract void DataSets(object dataDestination);
                /// <summary>
                /// Modifies retrieved data and Message data into a correct format for setting. (Optional.)
                /// </summary>
                public abstract void Modify();
                /// <summary>
                /// Parses the data retrieved from a data source in DataGets. (Optional.)
                /// </summary>
                public abstract void Parse();
                /// <summary>
                /// Validates received data for validity before attempting parsing, modification and setting. Should return true if not used.
                /// </summary>
                /// <returns>A boolean indicating if the received data is valid.</returns>
                public abstract bool Validate();
            }

            /// <summary>
            /// A static factory for the creation of message executors.
            /// </summary>
            public static class MessageExecutorFactory
            {
                /// <summary>
                /// Uses reflection to return the executor for the specified message.
                /// </summary>
                /// <param name = "message">The message you want to obtain an executor for.</param>
                /// <exception cref = "ArgumentNullException"><paramref name = "message"/> is <see langword = "null"/>.</exception>
                /// <exception cref = "AmbiguousMatchException">Unable to find executor for message with the type of <paramref name = "message"/>.</exception>
                /// <returns>The executor for this message.</returns>
                public static Skyline.DataMiner.Library.Common.InterAppCalls.MessageExecution.IMessageExecutor CreateExecutor(Skyline.DataMiner.Library.Common.InterAppCalls.CallSingle.Message message)
                {
                    if (message == null)
                    {
                        throw new System.ArgumentNullException("message");
                    }

                    System.Type concreteType = message.GetType();
                    System.Type concreteExecutor = null;
                    // Find the Concrete executor for this.
                    foreach (var assembly in Skyline.DataMiner.Library.Common.Reflection.ReflectionHelper.GetLoadedAssemblies())
                    {
                        if (concreteExecutor != null)
                            break;
                        concreteExecutor = FindTypeInAssembly(assembly, concreteType);
                    }

                    if (concreteExecutor != null)
                    {
                        return (Skyline.DataMiner.Library.Common.InterAppCalls.MessageExecution.IMessageExecutor)System.Activator.CreateInstance(concreteExecutor, message);
                    }
                    else
                    {
                        throw new System.Reflection.AmbiguousMatchException("Unable to find executor for message with type:" + concreteType + ". Verify you have a class implementing :MessageExecutor<" + concreteType + ">.");
                    }
                }

                private static System.Type FindTypeInAssembly(System.Reflection.Assembly assembly, System.Type concreteType)
                {
                    foreach (System.Type type in assembly.GetTypes())
                    {
                        if (type.IsInterface || type.IsAbstract || !type.BaseType.IsAbstract)
                            continue;
                        System.Type baseType = type.BaseType;
                        System.Type expectedBase = typeof(Skyline.DataMiner.Library.Common.InterAppCalls.MessageExecution.MessageExecutor<>);
                        if (baseType.Name == expectedBase.Name)
                        {
                            var genericType = baseType.GetGenericArguments()[0];
                            if (genericType == concreteType)
                            {
                                return type;
                            }
                        }
                    }

                    return null;
                }
            }
        }

        namespace Shared
        {
            /// <summary>
            /// Represents the location of a parameter that will hold the return message.
            /// </summary>
            public class ReturnAddress
            {
                /// <summary>
                ///  Initializes a new instance of the <see cref = "ReturnAddress"/> class.
                /// </summary>
                public ReturnAddress()
                {
                // Empty constructor necessary for serialization.
                }

                /// <summary>
                ///  Initializes a new instance of the <see cref = "ReturnAddress"/> class.
                /// </summary>
                /// <param name = "agentId">The DataMiner Agent ID.</param>
                /// <param name = "elementId">The element ID.</param>
                /// <param name = "pid">The parameter ID.</param>
                /// <exception cref = "ArgumentException">Agent, element and parameter IDs cannot be negative.</exception>
                public ReturnAddress(int agentId, int elementId, int pid)
                {
                    if (agentId < 0)
                        throw new System.ArgumentException("Agent Id cannot be a negative value.", "agentId");
                    if (elementId < 0)
                        throw new System.ArgumentException("Element Id cannot be a negative value.", "elementId");
                    if (pid < 0)
                        throw new System.ArgumentException("Parameter Id cannot be a negative value.", "pid");
                    ParameterId = pid;
                    AgentId = agentId;
                    ElementId = elementId;
                }

                /// <summary>
                /// Gets or sets the DataMiner Agent ID.
                /// </summary>
                /// <value>The DataMiner Agent ID.</value>
                public int AgentId
                {
                    get;
                    private set;
                }

                /// <summary>
                /// Gets or sets the element ID.
                /// </summary>
                /// <value>The element ID.</value>
                public int ElementId
                {
                    get;
                    private set;
                }

                /// <summary>
                /// Gets or sets the parameter ID.
                /// </summary>
                /// <value>The parameter ID.</value>
                public int ParameterId
                {
                    get;
                    private set;
                }
            }

            /// <summary>
            /// Represents the source of the inter-app call. This can include an element ID or just a string variable.
            /// </summary>
            public class Source
            {
                /// <summary>
                /// Initializes a new instance of the <see cref = "Source"/> class.
                /// </summary>
                public Source()
                {
                // Empty constructor necessary for Serialization.
                }

                /// <summary>
                /// Initializes a new instance of the <see cref = "Source"/> class using the specified name.
                /// </summary>
                /// <param name = "name">A textual representation of the source.</param>
                /// <exception cref = "ArgumentNullException">Name is <see langword = "null"/> empty or white space.</exception>
                public Source(string name)
                {
                    if (string.IsNullOrWhiteSpace(name))
                        throw new System.ArgumentNullException("name");
                    Name = name;
                }

                /// <summary>
                /// Initializes a new instance of the <see cref = "Source"/> class using the specified name, DataMiner Agent ID and element ID.
                /// </summary>
                /// <param name = "name">A textual representation of the source.</param>
                /// <param name = "agentId">The DataMiner Agent ID.</param>
                /// <param name = "elementId">The element ID.</param>
                /// <exception cref = "ArgumentNullException"><paramref name = "name"/> is <see langword = "null"/>.</exception>
                /// <exception cref = "ArgumentException"><paramref name = "agentId"/> or <paramref name = "elementId"/> is negative.</exception>
                public Source(string name, int agentId, int elementId)
                {
                    if (string.IsNullOrWhiteSpace(name))
                        throw new System.ArgumentNullException("name");
                    if (agentId < 0)
                        throw new System.ArgumentException("DataMiner Agent ID should not be negative.", "agentId");
                    if (elementId < 0)
                        throw new System.ArgumentException("Element Identifier should not be negative.", "elementId");
                    Name = name;
                    AgentId = agentId;
                    ElementId = elementId;
                }

                /// <summary>
                /// Gets or sets the DataMiner Agent ID.
                /// </summary>
                /// <value>The DataMiner Agent ID.</value>
                public int AgentId
                {
                    get;
                    set;
                }

                /// <summary>
                /// Gets or sets the element ID.
                /// </summary>
                /// <value>The element ID.</value>
                public int ElementId
                {
                    get;
                    set;
                }

                /// <summary>
                /// Gets or sets the textual representation of the source.
                /// </summary>
                /// <value>The textual representation of the source.</value>
                public string Name
                {
                    get;
                    set;
                }
            }
        }
    }

    internal static class Logger
    {
        private const long SizeLimit = 3 * 1024 * 1024;
        private const string LogFileName = @"C:\Skyline DataMiner\logging\ClassLibrary.txt";
        private const string LogPositionPlaceholder = "**********";
        private const int PlaceHolderSize = 10;
        private static long logPositionPlaceholderStart = -1;
        private static System.Threading.Mutex loggerMutex = new System.Threading.Mutex(false, "clpMutex");
        public static void Log(string message)
        {
            try
            {
                loggerMutex.WaitOne();
                string logPrefix = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff") + "|";
                long messageByteCount = System.Text.Encoding.UTF8.GetByteCount(message);
                // Safeguard for large messages.
                if (messageByteCount > SizeLimit)
                {
                    message = "WARNING: message \"" + message.Substring(0, 100) + " not logged as it is too large (over " + SizeLimit + " bytes).";
                }

                long limit = SizeLimit / 2; // Safeguard: limit messages. If safeguard removed, the limit would be: SizeLimit - placeholder size - prefix length - 4 (2 * CR LF).
                if (messageByteCount > limit)
                {
                    long overhead = messageByteCount - limit;
                    int partToRemove = (int)overhead / 4; // In worst case, each char takes 4 bytes.
                    if (partToRemove == 0)
                    {
                        partToRemove = 1;
                    }

                    while (messageByteCount > limit)
                    {
                        message = message.Substring(0, message.Length - partToRemove);
                        messageByteCount = System.Text.Encoding.UTF8.GetByteCount(message);
                    }
                }

                int byteCount = System.Text.Encoding.UTF8.GetByteCount(message);
                long positionOfPlaceHolder = GetPlaceHolderPosition();
                System.IO.Stream fileStream = null;
                try
                {
                    fileStream = new System.IO.FileStream(LogFileName, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite);
                    using (System.IO.StreamWriter sw = new System.IO.StreamWriter(fileStream))
                    {
                        fileStream = null;
                        if (positionOfPlaceHolder == -1)
                        {
                            sw.BaseStream.Position = 0;
                            sw.Write(logPrefix);
                            sw.WriteLine(message);
                            logPositionPlaceholderStart = byteCount + logPrefix.Length;
                            sw.WriteLine(LogPositionPlaceholder);
                        }
                        else
                        {
                            sw.BaseStream.Position = positionOfPlaceHolder;
                            if (positionOfPlaceHolder + byteCount + 4 + PlaceHolderSize > SizeLimit)
                            {
                                // Overwrite previous placeholder.
                                byte[] placeholder = System.Text.Encoding.UTF8.GetBytes("          ");
                                sw.BaseStream.Write(placeholder, 0, placeholder.Length);
                                sw.BaseStream.Position = 0;
                            }

                            sw.Write(logPrefix);
                            sw.WriteLine(message);
                            sw.Flush();
                            logPositionPlaceholderStart = sw.BaseStream.Position;
                            sw.WriteLine(LogPositionPlaceholder);
                        }
                    }
                }
                finally
                {
                    if (fileStream != null)
                    {
                        fileStream.Dispose();
                    }
                }
            }
            catch
            {
            // Do nothing.
            }
            finally
            {
                loggerMutex.ReleaseMutex();
            }
        }

        private static long SetToStartOfLine(System.IO.StreamReader streamReader, long startPosition)
        {
            System.IO.Stream stream = streamReader.BaseStream;
            for (long position = startPosition - 1; position > 0; position--)
            {
                stream.Position = position;
                if (stream.ReadByte() == '\n')
                {
                    return position + 1;
                }
            }

            return 0;
        }

        private static long GetPlaceHolderPosition()
        {
            long result = -1;
            System.IO.Stream fileStream = null;
            try
            {
                fileStream = System.IO.File.Open(LogFileName, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite);
                using (System.IO.StreamReader streamReader = new System.IO.StreamReader(fileStream))
                {
                    fileStream = null;
                    streamReader.DiscardBufferedData();
                    long startOfLinePosition = SetToStartOfLine(streamReader, logPositionPlaceholderStart);
                    streamReader.DiscardBufferedData();
                    streamReader.BaseStream.Position = startOfLinePosition;
                    string line;
                    long postionInFile = startOfLinePosition;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        if (line == LogPositionPlaceholder)
                        {
                            streamReader.DiscardBufferedData();
                            result = postionInFile;
                            break;
                        }
                        else
                        {
                            postionInFile = postionInFile + System.Text.Encoding.UTF8.GetByteCount(line) + 2;
                        }
                    }

                    // If this point is reached, it means the placeholder was still not found.
                    if (result == -1 && startOfLinePosition > 0)
                    {
                        streamReader.DiscardBufferedData();
                        streamReader.BaseStream.Position = 0;
                        while ((line = streamReader.ReadLine()) != null)
                        {
                            if (line == LogPositionPlaceholder)
                            {
                                streamReader.DiscardBufferedData();
                                result = streamReader.BaseStream.Position - PlaceHolderSize - 2;
                                break;
                            }
                        }
                    }
                }
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Dispose();
                }
            }

            return result;
        }
    }

    namespace Reflection
    {
        internal static class ReflectionHelper
        {
            private static readonly System.Collections.Concurrent.ConcurrentDictionary<System.String, System.Collections.Generic.List<System.Reflection.Assembly>> clpCashedAssemblies = new System.Collections.Concurrent.ConcurrentDictionary<System.String, System.Collections.Generic.List<System.Reflection.Assembly>>();
            public static System.Collections.Generic.List<System.Reflection.Assembly> GetLoadedAssemblies()
            {
                var clpAssembly = typeof(Skyline.DataMiner.Library.Common.Reflection.ReflectionHelper).Assembly.GetName().FullName;
                return clpCashedAssemblies.GetOrAdd(clpAssembly, _ => Load(clpAssembly));
            }

            private static System.Collections.Generic.List<System.Reflection.Assembly> Load(string clpAssembly)
            {
                System.Collections.Generic.List<System.Reflection.Assembly> loadedAssemblies = new System.Collections.Generic.List<System.Reflection.Assembly>();
                System.Diagnostics.Debug.WriteLine("CLP - InterApp - Load Assemblies");
                foreach (var assembly in System.AppDomain.CurrentDomain.GetAssemblies())
                {
                    if (assembly.ManifestModule.Name != "<In Memory Module>" && !assembly.FullName.StartsWith("System", System.StringComparison.Ordinal) && !assembly.FullName.StartsWith("Microsoft", System.StringComparison.Ordinal) && !assembly.FullName.StartsWith("mscorlib", System.StringComparison.Ordinal) && !assembly.FullName.StartsWith("SLNetTypes", System.StringComparison.Ordinal) && !assembly.FullName.StartsWith("SLManagedScripting", System.StringComparison.Ordinal) && !assembly.FullName.StartsWith("Newtonsoft.Json", System.StringComparison.Ordinal) && assembly.Location.IndexOf("App_Web", System.StringComparison.Ordinal) == -1 && assembly.Location.IndexOf("App_global", System.StringComparison.Ordinal) == -1 && assembly.FullName.IndexOf("CppCodeProvider", System.StringComparison.Ordinal) == -1 && assembly.FullName.IndexOf("WebMatrix", System.StringComparison.Ordinal) == -1 && assembly.FullName.IndexOf("SMDiagnostics", System.StringComparison.Ordinal) == -1)
                    {
                        var referencesClpAssembly = System.Linq.Enumerable.Contains(System.Linq.Enumerable.Select(assembly.GetReferencedAssemblies(), p => p.FullName), clpAssembly);
                        var thisAssembly = assembly.GetName().FullName;
                        if (referencesClpAssembly || thisAssembly == clpAssembly)
                        {
                            loadedAssemblies.Add(assembly);
                        }
                    }
                }

                return loadedAssemblies;
            }
        }
    }

    namespace Serializing
    {
        /// <summary>
        /// Interface that represents a serializer.
        /// </summary>
        public interface ISerializer
        {
            /// <summary>
            /// Deserializes the specified string into an object of type T.
            /// </summary>
            /// <typeparam name = "T">The type of the base class to deserialize into.</typeparam>
            /// <param name = "input">A string representing the serialized base class.</param>
            /// <returns>An instance of the selected base class.</returns>
            /// <exception cref = "ArgumentException"><paramref name = "input"/> had invalid format.</exception>
            T DeserializeFromString<T>(string input);
            /// <summary>
            /// Serializes the specified object in a string.
            /// </summary>
            /// <param name = "input">The object to serialize.</param>
            /// <returns>A string representing the serialized object.</returns>
            /// <exception cref = "ArgumentException"><paramref name = "input"/> had invalid format.</exception>
            string SerializeToString(object input);
        }

        /// <summary>
        /// A factory that creates some specific serializers.
        /// </summary>
        public static class SerializerFactory
        {
            /// <summary>
            /// Creates a serializer specifically for use by the InterApp module.
            /// </summary>
            /// <param name = "baseType">The type of the base class to serialize or deserialize.</param>
            /// <returns>An instance of type ISerializer.</returns>
            public static Skyline.DataMiner.Library.Common.Serializing.ISerializer CreateInterAppSerializer(System.Type baseType)
            {
                Skyline.DataMiner.Library.Common.Serializing.NoTagSerializing.SerializerBuilder builder = new Skyline.DataMiner.Library.Common.Serializing.NoTagSerializing.SerializerBuilder();
                builder.WithSerializer(Skyline.DataMiner.Library.Common.Serializing.NoTagSerializing.XmlSerializerType.JsonNewtonSoft);
                builder.WithBaseType(baseType);
                return builder.Build();
            }
        }

        namespace NoTagSerializing
        {
            internal class SerializerBuilder
            {
                private readonly System.Collections.Generic.List<System.Type> overrides = new System.Collections.Generic.List<System.Type>();
                private System.Type baseType;
                public Skyline.DataMiner.Library.Common.Serializing.ISerializer Build()
                {
                    return overrides.Count > 0 ? BuildWithOverrides() : BuildWithoutOverrides();
                }

                public Skyline.DataMiner.Library.Common.Serializing.NoTagSerializing.SerializerBuilder WithBaseType(System.Type t)
                {
                    baseType = t;
                    return this;
                }

                public Skyline.DataMiner.Library.Common.Serializing.NoTagSerializing.SerializerBuilder WithSerializer(Skyline.DataMiner.Library.Common.Serializing.NoTagSerializing.XmlSerializerType type)
                {
                    return this;
                }

                private Skyline.DataMiner.Library.Common.Serializing.ISerializer BuildWithoutOverrides()
                {
                    return baseType != null ? new Skyline.DataMiner.Library.Common.Serializing.NoTagSerializing.UsingJsonNewtonSoft.Serializer(baseType) : new Skyline.DataMiner.Library.Common.Serializing.NoTagSerializing.UsingJsonNewtonSoft.Serializer();
                }

                private Skyline.DataMiner.Library.Common.Serializing.ISerializer BuildWithOverrides()
                {
                    return baseType != null ? new Skyline.DataMiner.Library.Common.Serializing.NoTagSerializing.UsingJsonNewtonSoft.Serializer(baseType, overrides) : new Skyline.DataMiner.Library.Common.Serializing.NoTagSerializing.UsingJsonNewtonSoft.Serializer(overrides);
                }
            }

            internal enum XmlSerializerType
            {
                JsonNewtonSoft
            }

            namespace UsingJsonNewtonSoft
            {
                [Skyline.DataMiner.Library.Common.Attributes.DllImport("Newtonsoft.Json.dll")]
                internal class ContractResolverWithPrivates : Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver
                {
                    protected override Newtonsoft.Json.Serialization.JsonDictionaryContract CreateDictionaryContract(System.Type objectType)
                    {
                        Newtonsoft.Json.Serialization.JsonDictionaryContract contract = base.CreateDictionaryContract(objectType);
                        contract.DictionaryKeyResolver = key => key;
                        return contract;
                    }

                    protected override System.Collections.Generic.IList<Newtonsoft.Json.Serialization.JsonProperty> CreateProperties(System.Type type, Newtonsoft.Json.MemberSerialization memberSerialization)
                    {
                        return System.Linq.Enumerable.ToList(System.Linq.Enumerable.OrderBy(base.CreateProperties(type, memberSerialization), p => p.PropertyName));
                    }

                    protected override Newtonsoft.Json.Serialization.JsonProperty CreateProperty(System.Reflection.MemberInfo member, Newtonsoft.Json.MemberSerialization memberSerialization)
                    {
                        var prop = base.CreateProperty(member, memberSerialization);
                        if (!prop.Writable)
                        {
                            var property = member as System.Reflection.PropertyInfo;
                            if (property != null)
                            {
                                var hasPrivateSetter = property.GetSetMethod(true) != null;
                                prop.Writable = hasPrivateSetter;
                            }
                        }

                        return prop;
                    }
                }

                [Skyline.DataMiner.Library.Common.Attributes.DllImport("Newtonsoft.Json.dll")]
                internal class KnownTypesBinder : Newtonsoft.Json.Serialization.ISerializationBinder
                {
                    private readonly System.Collections.Generic.List<System.Reflection.Assembly> loadedAssemblies = new System.Collections.Generic.List<System.Reflection.Assembly>();
                    private System.Lazy<System.String[]> nonUniqueTypeNames;
                    public KnownTypesBinder()
                    {
                        if (loadedAssemblies.Count == 0)
                        {
                            loadedAssemblies = Skyline.DataMiner.Library.Common.Reflection.ReflectionHelper.GetLoadedAssemblies();
                        }
                    }

                    public KnownTypesBinder(System.Collections.Generic.IList<System.Type> knownTypes)
                    {
                        AddKnownTypes(knownTypes);
                    }

                    public System.Collections.Generic.IList<System.Type> KnownTypes
                    {
                        get;
                        private set;
                    }

                    public void AddKnownTypes(System.Collections.Generic.IList<System.Type> knownTypes)
                    {
                        if (knownTypes != null)
                        {
                            KnownTypes = knownTypes;
                            nonUniqueTypeNames = new System.Lazy<System.String[]>(() =>
                            {
                                return System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Select(System.Linq.Enumerable.Where(System.Linq.Enumerable.GroupBy(KnownTypes, x => x.Name), g => System.Linq.Enumerable.Count(g) > 1), y => y.Key));
                            }

                            );
                        }
                    }

                    public void BindToName(System.Type serializedType, out string assemblyName, out string typeName)
                    {
                        assemblyName = System.String.Empty;
                        if (serializedType == null)
                        {
                            throw new System.ArgumentNullException("serializedType");
                        }

                        if (KnownTypes != null && KnownTypes.Contains(serializedType) && !System.Linq.Enumerable.Contains(nonUniqueTypeNames.Value, serializedType.Name))
                        {
                            typeName = serializedType.Name;
                        }
                        else
                        {
                            typeName = serializedType.FullName;
                        }
                    }

#pragma warning disable S3776 // Cognitive Complexity of methods should not be too high

                    public System.Type BindToType(string assemblyName, string typeName)
#pragma warning restore S3776 // Cognitive Complexity of methods should not be too high

                    {
                        if (typeName == null)
                        {
                            throw new System.ArgumentNullException("typeName");
                        }

                        System.Type foundType = null;
                        bool array = false;
                        // To Deal with Arrays:
                        if (typeName.EndsWith("[]", System.StringComparison.Ordinal))
                        {
#pragma warning disable S1226 // Method parameters, caught exceptions and foreach variables' initial values should not be ignored

#pragma warning disable S3257 // Declarations and initializations should be as concise as possible

                            typeName = typeName.TrimEnd(new char[]{'[', ']'});
#pragma warning restore S3257 // Declarations and initializations should be as concise as possible

#pragma warning restore S1226 // Method parameters, caught exceptions and foreach variables' initial values should not be ignored

                            array = true;
                        }

                        if (typeName.StartsWith("System", System.StringComparison.Ordinal) && foundType == null)
                        {
                            // MSCORLIB
                            var mscorlibAssembly = typeof(System.Object).Assembly;
                            try
                            {
                                foundType = mscorlibAssembly.GetType(typeName);
                            }
                            catch
                            {
                            // Ignore exception in order to see if we find the type. Need to use this for logic unfortunately.
                            }

                            if (foundType == null)
                            {
                                // SYSTEM.CORE
                                var sysCoreAssembly = typeof(System.Uri).Assembly;
                                try
                                {
                                    foundType = sysCoreAssembly.GetType(typeName);
                                }
                                catch
                                {
                                // Ignore exception in order to see if we find the type. Need to use this for logic unfortunately.
                                }
                            }
                        }

                        if (KnownTypes != null)
                        {
                            try
                            {
                                foundType = System.Linq.Enumerable.SingleOrDefault(KnownTypes, t => t.Name == typeName);
                            }
                            catch (System.InvalidOperationException ex)
                            {
                                throw (new Skyline.DataMiner.Library.Common.IncorrectDataException("Type Name: " + typeName + " was unique on serialization side but not on deserialization side. Please verify the same KnownTypes List is used on both ends of the communication.", ex));
                            }
                        }

                        if (KnownTypes != null && foundType == null)
                        {
                            foundType = System.Linq.Enumerable.SingleOrDefault(KnownTypes, t => t.FullName == typeName);
                        }

                        if (foundType == null)
                        {
                            // Checks the calling assemblies.
                            foreach (var ass in loadedAssemblies)
                            {
                                try
                                {
                                    foundType = ass.GetType(typeName);
                                }
                                catch
                                {
                                // Ignore exception in order to see if we find the type. Need to use this for logic unfortunately.
                                }

                                if (foundType == null)
                                {
                                    foundType = System.Linq.Enumerable.FirstOrDefault(ass.GetTypes(), p => p.FullName == typeName);
                                }

                                if (foundType != null)
                                    break;
                            }
                        }

                        if (foundType == null)
                        {
                            // Checks the current assembly.
                            foreach (System.Type t in typeof(Skyline.DataMiner.Library.Common.Serializing.NoTagSerializing.UsingJsonNewtonSoft.KnownTypesBinder).Assembly.GetTypes())
                            {
                                if (typeName == t.FullName)
                                {
                                    foundType = t;
                                    break;
                                }
                            }
                        }

                        if (foundType == null)
                        {
                            Newtonsoft.Json.Serialization.DefaultSerializationBinder def = new Newtonsoft.Json.Serialization.DefaultSerializationBinder();
                            if (System.String.IsNullOrWhiteSpace(assemblyName))
                            {
#pragma warning disable S1226 // Method parameters, caught exceptions and foreach variables' initial values should not be ignored

                                assemblyName = typeof(Skyline.DataMiner.Library.Common.Serializing.NoTagSerializing.UsingJsonNewtonSoft.KnownTypesBinder).Assembly.GetName().Name;
#pragma warning restore S1226 // Method parameters, caught exceptions and foreach variables' initial values should not be ignored

                            }

                            foundType = def.BindToType(assemblyName, typeName);
                        }

                        if (array)
                        {
                            foundType = foundType.MakeArrayType();
                        }

                        return foundType;
                    }
                }

                [Skyline.DataMiner.Library.Common.Attributes.DllImport("Newtonsoft.Json.dll")]
                internal class Serializer : Skyline.DataMiner.Library.Common.Serializing.ISerializer
                {
                    public Serializer()
                    {
                        KnownTypes = new Skyline.DataMiner.Library.Common.Serializing.NoTagSerializing.UsingJsonNewtonSoft.KnownTypesBinder();
                        ApplySettings();
                    }

                    public Serializer(System.Collections.Generic.List<System.Type> knownTypes)
                    {
                        KnownTypes = new Skyline.DataMiner.Library.Common.Serializing.NoTagSerializing.UsingJsonNewtonSoft.KnownTypesBinder(knownTypes);
                        ApplySettings();
                    }

                    public Serializer(System.Type rootType, System.Collections.Generic.List<System.Type> knownTypes = null)
                    {
                        RootType = rootType;
                        KnownTypes = knownTypes != null ? new Skyline.DataMiner.Library.Common.Serializing.NoTagSerializing.UsingJsonNewtonSoft.KnownTypesBinder(knownTypes) : new Skyline.DataMiner.Library.Common.Serializing.NoTagSerializing.UsingJsonNewtonSoft.KnownTypesBinder();
                        ApplySettings();
                    }

                    public Skyline.DataMiner.Library.Common.Serializing.NoTagSerializing.UsingJsonNewtonSoft.KnownTypesBinder KnownTypes
                    {
                        get;
                        private set;
                    }

                    public System.Type RootType
                    {
                        get;
                        private set;
                    }

                    public Newtonsoft.Json.JsonSerializerSettings Settings
                    {
                        get;
                        private set;
                    }

                    public T DeserializeFromString<T>(string input)
                    {
                        try
                        {
                            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(input, Settings);
                        }
                        catch (System.Exception e)
                        {
                            throw new System.ArgumentException("Cannot Json Deserialize: " + input, "input", e);
                        }
                    }

                    public string SerializeToString(object input)
                    {
                        try
                        {
                            return RootType != null ? Newtonsoft.Json.JsonConvert.SerializeObject(input, RootType, Settings) : Newtonsoft.Json.JsonConvert.SerializeObject(input, Settings);
                        }
                        catch (System.Exception e)
                        {
                            throw new System.ArgumentException("Cannot Json Serialize the provided object", "input", e);
                        }
                    }

                    private void ApplySettings()
                    {
                        Settings = new Newtonsoft.Json.JsonSerializerSettings{SerializationBinder = KnownTypes, TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto, TypeNameAssemblyFormatHandling = Newtonsoft.Json.TypeNameAssemblyFormatHandling.Full, MetadataPropertyHandling = Newtonsoft.Json.MetadataPropertyHandling.ReadAhead, ObjectCreationHandling = Newtonsoft.Json.ObjectCreationHandling.Replace, MissingMemberHandling = Newtonsoft.Json.MissingMemberHandling.Ignore, ContractResolver = new Skyline.DataMiner.Library.Common.Serializing.NoTagSerializing.UsingJsonNewtonSoft.ContractResolverWithPrivates(), PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects};
                    }
                }
            }
        }
    }
}