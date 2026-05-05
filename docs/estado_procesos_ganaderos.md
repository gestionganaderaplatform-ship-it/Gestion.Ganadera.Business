# Estado de Procesos Ganaderos

Este documento centraliza el estado de implementación de los procesos de negocio en los tres niveles: Backend (Business API), Frontend (Web) y documentación funcional.

## Matriz de Seguimiento

| Proceso | Fase | Backend | Frontend | Estado Global |
| :--- | :---: | :--- | :--- | :---: |
| **Registro de existente** | 1 | ✅ Completo (Service, Repo, Validator, Controller) | ✅ Completo (Modernizado: Drafts, Validaciones, Steps) | ✅ |
| **Compra** | 1 | ✅ Completo (Service, Repo, Validator, Controller) | ✅ Completo (Modernizado: Drafts, Validaciones, Steps) | ✅ |
| **Nacimiento** | 1 | ✅ Completo (Service, Repo, Validator, Controller) | ✅ Completo (Modernizado: Drafts, Validaciones, Steps) | ✅ |
| **Muerte** | 1 | ✅ Completo (Service, Repo, Validator, Controller) | ✅ Completo (Modernizado y corregido: envía Finca_Codigo) | ✅ |
| **Pesaje** | 1 | ✅ Completo (Service actualizado, Repo, Validator, Controller) | ⚠️ Parcial (Esqueleto base, sin lógica ni API Service) | ⚠️ |
| **Movimiento de potrero** | 1 | ✅ Completo (Service, Repo, Validator, Controller) | ⚠️ Parcial (Esqueleto base, sin lógica de selección/confirmación) | ⚠️ |
| **Venta** | 1 | ✅ Completo (Service, Repo, Validator, Controller) | ⚠️ Parcial (Esqueleto base, sin lógica ni API Service) | ⚠️ |
| **Vacunación** | 2 | ✅ Completo (Service con lote, Repo, Validator, Controller) | ⚠️ Parcial (Esqueleto base, sin conexión al backend) | ⚠️ |
| **Traslado entre fincas** | 1 | ✅ Completo (Service, Repo, Validator, Controller) | ⚠️ Parcial (Esqueleto base) | ⚠️ |
| **Tratamiento sanitario** | 2 | ✅ Completo (Service, Repo, Validator, Controller) | ⚠️ Parcial (Esqueleto base) | ✅ |
| **Palpación o revisión** | 2 | ✅ Completo (Service, Repo, Validator, Controller) | ⚠️ Parcial (Esqueleto base) | ✅ |
| **Destete** | 2 | ✅ Completo (Service, Repo, Validator, Controller - corregido) | ⚠️ Parcial (Esqueleto base) | ✅ |
| **Descarte** | 2 | ✅ Completo (Service, Repo, Validator, Controller - corregido) | ⚠️ Parcial (Esqueleto base) | ✅ |
| **Cambio de categoría** | 2 | ⬜ Pendiente (Sin código) | ⚠️ Parcial (Esqueleto base) | ⬜ |

## Próximos Pasos (Enfoque Backend)

La prioridad actual es completar el desarrollo de la lógica de negocio y persistencia para los procesos pendientes de la Fase 2.

### 1. Cambio de categoría (Fase 2)
- [ ] Definir entidad de detalle.
- [ ] Implementar Repository, Service y Controller.

---
*Ultima actualización: 2026-05-04(actualizado)*
