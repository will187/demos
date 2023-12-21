using AutoMapper;

namespace Application.Common.Mappings;

public interface IMapFrom
{
    public interface IMapFrom<T>
    {
        void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
    }
}
