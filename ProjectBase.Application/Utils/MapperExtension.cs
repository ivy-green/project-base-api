using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace ProjectBase.Application.Utils
{
    public static class MapperExtension
    {
        public static TEntity ProjectToEntity<TEntity, TDto>(this TDto dto)
        {
            if(dto == null)
            {
                return default;
            }

            var entityType = typeof (TEntity);
            var dtoType = typeof(TDto);
            var dtoProps = dtoType.GetProperties();

            var entity = Activator.CreateInstance(entityType);
            foreach(var prop in dtoProps)
            {
                var entityProp = entityType.GetProperty(prop.Name);
                if(entityProp == null || 
                    entityProp.GetCustomAttributes<NotMappedAttribute>() != null ||
                    prop.GetCustomAttributes<NotMappedAttribute>() != null)
                {
                    continue;
                }

                var value = prop.GetValue(dto, null);
                entityType.GetProperty(prop.Name)?.SetValue(dto, value);


            }
            return (TEntity)entity;
        }

        public static TDto ProjectToDto<TEntity, TDto>(this TEntity entity)
        {
            if (entity == null)
            {
                return default;
            }

            var dtoType = typeof(TDto);
            var entityType = typeof(TEntity);
            var entityProps = entityType.GetProperties();

            var dto = Activator.CreateInstance(entityType);
            foreach (var prop in entityProps)
            {
                var dtoProp = entityType.GetProperty(prop.Name);
                if (dtoProp == null ||
                    dtoProp.GetCustomAttributes<NotMappedAttribute>() != null ||
                    prop.GetCustomAttributes<NotMappedAttribute>() != null)
                {
                    continue;
                }

                var value = prop.GetValue(dto, null);
                dtoType.GetProperty(prop.Name)?.SetValue(dto, value);


            }
            return (TDto)dto;
        }

        public static List<TEntity> ProjectToEntities<TEntity, TDto>(this List<TDto> dtos)
        {
            if (dtos == null || !dtos.Any())
            {
                return new List<TEntity>();
            }

            var entityType = typeof(TEntity);
            var dtoType = typeof(TDto);
            var dtoPros = dtoType.GetProperties();
            //var entity = Activator.CreateInstance(entityType);

            var entities = new List<TEntity>();

            foreach (var dto in dtos)
            {
                var entity = Activator.CreateInstance(entityType);
                foreach (var prop in dtoPros)
                {
                    var entityPro = entityType.GetProperty(prop.Name);
                    if (entityPro == null ||
                        entityPro.GetCustomAttribute<NotMappedAttribute>() != null ||
                        prop.GetCustomAttribute<NotMappedAttribute>() != null)
                    {
                        continue;
                    }

                    var valor = prop.GetValue(dto, null);
                    entityType.GetProperty(prop.Name)?.SetValue(entity, valor);
                }
                entities.Add((TEntity)entity);
            }
            return entities;
        }

        public static List<TDto> ProjectToDTOs<TEntity, TDto>(this List<TEntity> entities)
        {
            if (entities == null || !entities.Any())
            {
                return new List<TDto>();
            }

            var dtoType = typeof(TDto);
            var entityType = typeof(TEntity);
            var entityProperties = entityType.GetProperties();

            var dtos = new List<TDto>();

            foreach (var entity in entities)
            {
                var dto = Activator.CreateInstance(dtoType);
                foreach (var entityProp in entityProperties)
                {
                    var dtoProp = dtoType.GetProperty(entityProp.Name);
                    if (dtoProp == null ||
                        dtoProp.GetCustomAttribute<NotMappedAttribute>() != null ||
                        entityProp.GetCustomAttribute<NotMappedAttribute>() != null)
                    {
                        continue;
                    }

                    var entityValue = entityProp.GetValue(entity, null);
                    dtoProp.SetValue(dto, entityValue);
                }
                dtos.Add((TDto)dto);
            }
            return dtos;
        }

        public static List<string> ToList(this string jsonString)
        {
            return string.IsNullOrEmpty(jsonString) ? new List<string>() : JsonConvert.DeserializeObject<List<string>>(jsonString);
        }
    }
}
