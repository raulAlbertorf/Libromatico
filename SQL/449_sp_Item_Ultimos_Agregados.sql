--Delimiter $$
DROP PROCEDURE IF EXISTS sp_Item_Ultimos_Agregados $$

CREATE PROCEDURE sp_Item_Ultimos_Agregados (
	inCantidad BIGINT
)
BEGIN
	SELECT
		Id,
		Titulo,
		Disponibilidad,
		Resumen,
		VecesVisto,
		Perfil_Id,
		UrlImagen
	FROM
		Item
	ORDER BY 
		Id DESC
	LIMIT
		inCantidad;
END
$$