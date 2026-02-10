# Documentación de Consultas: `wsServicioFiniquitos.svc.cs`

## Contexto
Esta clase implementa el contrato de servicio WCF.

## Consultas
**No contiene consultas SQL ni llamadas a procedimientos almacenados directas.**

## Función
Delega todas las operaciones de datos a las clases de lógica de negocio (`CPagos`, `CFiniquitos`, `CSolB4J`, etc.), actuando solo como pasarela (Facade).
