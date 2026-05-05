# Proceso · Palpación o revisión reproductiva

## 1. Objetivo
Registrar revisiones reproductivas y su resultado para dar trazabilidad a la condición reproductiva del animal.

## 2. Alcance
Cubre:
- registro individual
- fecha de revisión
- resultado
- responsable
- observación y datos complementarios
- base para alertas o seguimiento futuro

No cubre:
- manejo reproductivo completo
- programación automática compleja
- diagnósticos clínicos avanzados

## 3. Roles que intervienen
- dueño titular
- administrador de finca
- veterinario
- operario autorizado solo si la política lo permite

## 4. Disparador del proceso
Se inicia cuando se realiza una palpación o revisión reproductiva y se requiere formalizar su resultado.

## 5. Precondiciones
- finca activa
- animal existente y activo
- usuario con permiso
- animal elegible según sexo y categoría cuando aplique

## 6. Datos de entrada
- animal
- fecha_revision
- resultado
- responsable_revision
- observacion
- dato_complementario_reproductivo

## 7. Campos exactos
- animal
- fecha_revision
- resultado
- responsable_revision
- observacion
- dato_complementario_reproductivo
- usuario_registro
- fecha_hora_registro
- tipo_evento = revision_reproductiva

## 8. Obligatorios y opcionales
### Obligatorios
- animal
- fecha_revision
- resultado

### Opcionales
- responsable_revision
- observacion
- dato_complementario_reproductivo

## 9. Valores por defecto
- fecha_revision: hoy
- responsable_revision: vacío
- observacion: vacía
- dato_complementario_reproductivo: vacío

## 10. Reglas de validación
1. El animal debe existir y estar activo.
2. El animal debe ser elegible si la regla reproductiva lo exige.
3. La fecha es obligatoria.
4. El resultado es obligatorio.
5. El proceso debe cerrar con confirmación final.
6. La fecha debe respetar política de fecha anterior.

## 11. Bloqueos
- no hay animal seleccionado
- no hay fecha
- no hay resultado
- animal inexistente
- animal inactivo
- animal no elegible
- usuario sin permiso

## 12. Advertencias
- faltan datos complementarios
- fecha muy antigua
- resultado amerita seguimiento
- el mismo animal ya tiene revisión muy reciente

## 13. Mensajes funcionales
- Debe seleccionar un animal.
- La fecha de la revisión es obligatoria.
- Debe registrar un resultado.
- El animal no es elegible para este proceso.
- No puede registrar una revisión reproductiva para un animal inactivo.
- No tiene permiso para registrar este proceso.

## 14. Flujo principal paso a paso
1. Entrar a Registrar.
2. Elegir Palpación o revisión reproductiva.
3. Seleccionar animal.
4. El sistema valida elegibilidad.
5. Registrar fecha.
6. Registrar resultado.
7. Registrar responsable y datos opcionales.
8. Mostrar resumen final.
9. Confirmar.
10. Guardar.

## 15. Escenarios alternos
- si el animal no es elegible, se bloquea
- si no se conoce el responsable exacto, puede quedar vacío
- si el resultado implica seguimiento, se deja base para control posterior

## 16. Correcciones y anulaciones
- corrección auditada
- anulación con justificación si la política lo requiere
- operario no anula
- veterinario, administrador y dueño sí según permisos

## 17. Resultado esperado
Queda trazado el resultado reproductivo del animal.

## 18. Impacto en historial
- tipo_evento = revision_reproductiva
- fecha_evento
- fecha_registro
- usuario_registro
- resultado
- responsable
- observacion

## 19. Impacto en estados derivados del animal
No cambia estado activo ni ubicación.
Puede actualizar:
- último resultado reproductivo
- alertas o seguimientos futuros
- lectura resumida reproductiva

## 20. Observaciones de diseño funcional
- resultado debe venir de catálogo controlado
- elegibilidad por sexo y categoría debe quedar centralizada y no dispersa

## 21. Maestros requeridos
- **PalpacionResultado**: Catálogo controlado de diagnósticos reproductivos (Ej: Preñada, Vacía, En duda, Quiste Folicular, Cuerpo Lúteo, Involución Uterina).
- **ResponsableRevision**: (Opcional) Catálogo de veterinarios o personal técnico autorizado.

## 22. Impacto en estados derivados del animal
Para mantener el patrón de "Snapshot" del sistema, se actualizarán los siguientes campos en la entidad **Animal**:
- **Animal_Ultima_Palpacion_Fecha**: Fecha de la última revisión realizada.
- **Animal_Ultimo_Resultado_Reproductivo**: El nombre del resultado obtenido (Ej: "Preñada").
- **Animal_Estado_Reproductivo_Actual**: Estado simplificado derivado (Ej: "Preñada", "Abierta").

## 23. Reglas de Elegibilidad (Backend)
- **Sexo**: Únicamente animales de sexo "Hembra" (H).
- **Edad/Categoría**: Animales que pertenezcan a categorías reproductivas (Novilla de Vientre, Vaca Parida, Vaca Horra).
