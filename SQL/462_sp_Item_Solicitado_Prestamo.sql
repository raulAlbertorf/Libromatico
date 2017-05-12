--DELIMITER $$
DROP PROCEDURE IF EXISTS sp_Item_Solicitado_Prestamo $$

CREATE PROCEDURE sp_Item_Solicitado_Prestamo(
	inPerfil_Id BIGINT,
	inItem_Id BIGINT
)
BEGIN
	SELECT
		P.Id AS Prestamo_Id
	FROM 
		Prestamo AS P
	WHERE 
		P.Item_Id = inItem_Id 
		AND 
		P.Receptor_Id = inPerfil_Id;
END
$$