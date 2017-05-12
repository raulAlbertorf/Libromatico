--Delimiter $$
DROP PROCEDURE IF EXISTS sp_Estanteria_Buscar_Propiedad$$

CREATE PROCEDURE sp_Estanteria_Buscar_Propiedad (
	inValue VARCHAR (255),
    inOffset INT,
	inLimit INT
)
BEGIN
	SELECT 
		DISTINCT I.Id	AS	Item_Id,
		I.Titulo AS Item_Titulo,
        I.Resumen AS Item_Resumen,
        I.VecesVisto AS Item_VecesVisto,
        I.UrlImagen  AS Item_UrlImagen,
        I.Perfil_Id	AS Perfil_Id,
        P.Nombre	AS	Perfil_Nombre,
        P.UrlImagen	AS	Perfil_UrlImagen,
        P.Nacionalidad	AS Perfil_Nacionalidad
FROM 
item AS I
    INNER JOIN Perfil AS P
		ON I.Perfil_Id = P.Id 
    INNER JOIN Propiedad AS Pro
		ON Pro.Item_Id = I.Id	
WHERE 
    I.Disponibilidad = 1
    AND
    (
        Pro.ValorPropiedad LIKE CONCAT('%', inValue , '%')
        OR
        Pro.NombrePropiedad LIKE CONCAT('%', inValue , '%')
    )
    
LIMIT 
    inOffset, inLimit;
END
$$