--Delimiter $$
DROP PROCEDURE IF EXISTS sp_Item_LosMasVisto $$

CREATE PROCEDURE sp_Item_LosMasVisto (
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
		VecesVisto DESC
	LIMIT
		inCantidad;
END
$$