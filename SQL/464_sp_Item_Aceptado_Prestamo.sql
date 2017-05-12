--DELIMITER $$
DROP PROCEDURE IF EXISTS sp_Item_Aceptado_Prestamo $$

CREATE PROCEDURE sp_Item_Aceptado_Prestamo(
	inPerfil_Id BIGINT,
	inItem_Id BIGINT
)
BEGIN
	SELECT
		P.Id AS Prestamo_Id,
		P.Estado AS Prestamo_Estado
	FROM 
		Prestamo AS P
	WHERE 
		P.Item_Id = inItem_Id
		AND 
		P.Prestamista_Id = inPerfil_Id
		AND (P.Estado = 2 OR P.Estado = 3);
END
$$