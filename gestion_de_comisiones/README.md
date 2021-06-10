
## base de datos correr primera vez

Scaffold-DbContext "Server=10.2.10.15;Database=BDMultinivel; User Id=sa;password=Passw0rd;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir MultinivelModel 

## base de datos Actualizar modelo

Scaffold-DbContext "Server=10.2.10.15;Database=BDMultinivel; User Id=sa;password=Passw0rd;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir MultinivelModel -F
## se instalo package nuGet un nuevo SeriLog.Extensions.Loggin.File
