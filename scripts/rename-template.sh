#!/usr/bin/env bash
set -euo pipefail

OLD_SOLUTION_NAME="Gestion.Ganadera"
OLD_DB_CONTEXT="AppDbContext"
OLD_DB_NAME="AppDb"

NEW_SOLUTION_NAME="${1:-}"
NEW_DB_CONTEXT="${2:-AppDbContext}"
NEW_DB_NAME="${3:-AppDb}"

if [[ -z "$NEW_SOLUTION_NAME" ]]; then
  echo "Uso: bash scripts/rename-template.sh <Nuevo.Nombre.Solucion> [NuevoDbContext=AppDbContext] [NuevoNombreDb=AppDb]"
  exit 1
fi

for value in "$NEW_SOLUTION_NAME" "$NEW_DB_CONTEXT" "$NEW_DB_NAME"; do
  if [[ "$value" == *"/"* || "$value" == *"\\"* ]]; then
    echo "Error: los valores no deben contener separadores de ruta."
    exit 1
  fi
done

echo "[0/4] Eliminando carpeta .git si existe..."

if [[ -d ".git" ]]; then
  rm -rf .git
  echo ".git eliminada."
else
  echo "No existe carpeta .git."
fi

echo "[1/4] Reemplazando texto en archivos..."

while IFS= read -r -d '' file; do
  perl -0pi -e "s/\Q$OLD_SOLUTION_NAME\E/$NEW_SOLUTION_NAME/g; s/\Q$OLD_DB_CONTEXT\E/$NEW_DB_CONTEXT/g; s/\Q$OLD_DB_NAME\E/$NEW_DB_NAME/g" "$file"
done < <(find . \
  -type f \
  -not -path "*/bin/*" \
  -not -path "*/obj/*" \
  -not -path "*/.git/*" \
  -print0)

echo "[2/4] Renombrando archivos..."

while IFS= read -r -d '' path; do
  new_path="$path"
  new_path="${new_path//$OLD_SOLUTION_NAME/$NEW_SOLUTION_NAME}"
  new_path="${new_path//$OLD_DB_CONTEXT/$NEW_DB_CONTEXT}"
  new_path="${new_path//$OLD_DB_NAME/$NEW_DB_NAME}"

  if [[ "$path" != "$new_path" ]]; then
    mkdir -p "$(dirname "$new_path")"
    mv "$path" "$new_path"
  fi
done < <(find . -depth -type f \( -name "*${OLD_SOLUTION_NAME}*" -o -name "*${OLD_DB_CONTEXT}*" -o -name "*${OLD_DB_NAME}*" \) -print0)

echo "[3/4] Renombrando carpetas..."

while IFS= read -r -d '' path; do
  new_path="$path"
  new_path="${new_path//$OLD_SOLUTION_NAME/$NEW_SOLUTION_NAME}"
  new_path="${new_path//$OLD_DB_CONTEXT/$NEW_DB_CONTEXT}"
  new_path="${new_path//$OLD_DB_NAME/$NEW_DB_NAME}"

  if [[ "$path" != "$new_path" ]]; then
    mv "$path" "$new_path"
  fi
done < <(find . -depth -type d \( -name "*${OLD_SOLUTION_NAME}*" -o -name "*${OLD_DB_CONTEXT}*" -o -name "*${OLD_DB_NAME}*" \) -print0)

echo "Proceso terminado."
