namespace Exporter.Excel;

public static class ExcelTemplate
{
    public static readonly string Template = """
                                             using System;
                                             using ProtoBuf;
                                             using Fantasy;
                                             using System.Linq;
                                             using System.Collections.Generic;
                                             // ReSharper disable CollectionNeverUpdated.Global
                                             // ReSharper disable UnusedAutoPropertyAccessor.Global
                                             #pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
                                             #pragma warning disable CS0169
                                             #pragma warning disable CS8618
                                             #pragma warning disable CS8625
                                             #pragma warning disable CS8603
                                             
                                             namespace (namespace)
                                             {
                                                 [ProtoContract]
                                                 public sealed partial class (ConfigName)Data :  AProto, IConfigTable, IDisposable
                                                 {
                                                     [ProtoMember(1)]
                                                     public List<(ConfigName)> List { get; set; } = new List<(ConfigName)>();
                                                     [ProtoIgnore]
                                                     private readonly Dictionary<uint, (ConfigName)> _configs = new Dictionary<uint, (ConfigName)>();
                                                     private static (ConfigName)Data _instance;
                                             
                                                     public static (ConfigName)Data Instance
                                                     {
                                                         get { return _instance ??= ConfigTableHelper.Instance.Load<(ConfigName)Data>(); } 
                                                         private set => _instance = value;
                                                     }
                                             
                                                     public (ConfigName) Get(uint id, bool check = true)
                                                     {
                                                         if (_configs.ContainsKey(id))
                                                         {
                                                             return _configs[id];
                                                         }
                                                 
                                                         if (check)
                                                         {
                                                             throw new Exception($"(ConfigName) not find {id} Id");
                                                         }
                                                         
                                                         return null;
                                                     }
                                                     public bool TryGet(uint id, out (ConfigName) config)
                                                     {
                                                         config = null;
                                                         
                                                         if (!_configs.ContainsKey(id))
                                                         {
                                                             return false;
                                                         }
                                                             
                                                         config = _configs[id];
                                                         return true;
                                                     }
                                                     public override void AfterDeserialization()
                                                     {
                                                         for (var i = 0; i < List.Count; i++)
                                                         {
                                                             (ConfigName) config = List[i];
                                                             _configs.Add(config.Id, config);
                                                             config.AfterDeserialization();
                                                         }
                                                 
                                                         base.AfterDeserialization();
                                                     }
                                                     
                                                     public void Dispose()
                                                     {
                                                         Instance = null;
                                                     }
                                                 }
                                                 
                                                 [ProtoContract]
                                                 public sealed partial class (ConfigName) : AProto
                                                 {(Fields)        		     
                                                 } 
                                             }  
                                             """;
}