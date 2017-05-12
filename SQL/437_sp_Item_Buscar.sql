--Delimiter $$
DROP PROCEDURE IF EXISTS sp_Item_Buscar $$

CREATE PROCEDURE sp_Item_Buscar(
	inTermino VARCHAR(256),
	inOffset INT,
	inLimit INT
)
BEGIN
	SELECT
		I.Id AS Item_Id,
		I.Titulo AS Item_Titulo,
		I.Resumen AS Item_Resumen,
		I.VecesVisto AS Item_VecesVisto,
		I.Perfil_Id AS Perfil_Id,
		I.UrlImagen AS Item_UrlImagen,
		P.Nombre AS Perfil_Nombre,
		P.UrlImagen AS Perfil_UrlImagen,
		P.Nacionalidad AS Perfil_Nac
	FROM
		Item AS I, 
		Perfil AS P
	WHERE
		I.Perfil_Id = P.Id AND I.Disponibilidad = 1
	AND
	(
		I.Titulo LIKE CONCAT ('%',inTermino,'%')
		OR
		I.Resumen LIKE CONCAT ('%',inTermino,'%')
	) 
LIMIT
	inOffset, inLimit; 
END
$$