# Hata ayıklama kapsayıcınızı özelleştirme ve Visual Studio’nun daha hızlı hata ayıklama için görüntülerinizi derlemek üzere bu Dockerfile'ı nasıl kullandığı hakkında bilgi edinmek için https://aka.ms/customizecontainer sayfasına bakın.

# Bu aşama, VS'den hızlı modda çalıştırıldığında kullanılır (Hata ayıklama yapılandırması için varsayılan olarak ayarlıdır)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# Bu aşama, hizmet projesini oluşturmak için kullanılır
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Presentation/HotelReservationApi.Presentation/HotelReservationApi.Presentation.csproj", "Presentation/HotelReservationApi.Presentation/"]
COPY ["Core/HotelReservationApi.Application/HotelReservationApi.Application.csproj", "Core/HotelReservationApi.Application/"]
COPY ["Core/HotelReservationApi.Domain/HotelReservationApi.Domain.csproj", "Core/HotelReservationApi.Domain/"]
COPY ["Infrastructure/HotelReservationApi.Persistence/HotelReservationApi.Persistence.csproj", "Infrastructure/HotelReservationApi.Persistence/"]
RUN dotnet restore "./Presentation/HotelReservationApi.Presentation/HotelReservationApi.Presentation.csproj"
COPY . .
WORKDIR "/src/Presentation/HotelReservationApi.Presentation"
RUN dotnet build "./HotelReservationApi.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Bu aşama, son aşamaya kopyalanacak hizmet projesini yayımlamak için kullanılır
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./HotelReservationApi.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Bu aşama üretimde veya VS'den normal modda çalıştırıldığında kullanılır (Hata Ayıklama yapılandırması kullanılmazken varsayılan olarak ayarlıdır)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HotelReservationApi.Presentation.dll"]