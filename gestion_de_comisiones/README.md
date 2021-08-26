
## base de datos correr primera vez

Scaffold-DbContext "Server=10.2.10.15;Database=BDMultinivel; User Id=sa;password=Passw0rd;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir MultinivelModel 

## base de datos Actualizar modelo

Scaffold-DbContext "Server=10.2.10.15;Database=BDMultinivel; User Id=sa;password=Passw0rd;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir MultinivelModel -F

## se instalo package nuGet un nuevo SeriLog.Extensions.Loggin.File
## reemplazar <aspNetCore> en el web config, al ser compilado para ser, PARA QUE PUEDA IMPRIMIR logs Y ASEGURARSE QUE LA PUBLICACION EN EL IIS ESTE EN EL GRUPO DE "DefaultAppPool" YA QUE ESTE TIENE ACCESO DE LECTURA Y ESCRITURA. 

      <aspNetCore processPath="dotnet" arguments=".\gestion_de_comisiones.dll" stdoutLogEnabled="true" stdoutLogFile=".\Logs\stdout" hostingModel="inprocess">
        <handlerSettings>
         <handlerSetting name="debugFile" value=".\Logs\aspnetcore-debug.log" />
         <handlerSetting name="debugLevel" value="FILE,TRACE" />
        </handlerSettings>
      </aspNetCore>
