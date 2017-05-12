--Delimiter $$
DROP PROCEDURE IF EXISTS sp_Item_Seleccionar_Items_Termino_Ubicacion $$
CREATE PROCEDURE sp_Item_Seleccionar_Items_Termino_Ubicacion (
	inTermino VARCHAR(256),
    inUbicacion_Latitude float(10,6),
    inUbicacion_Longitude float(10,6),
    inRadio	INT,
    inOffset INT,
	inLimit INT
)
BEGIN
SELECT 
 	I.Id			AS 	Item_Id,
    I.Titulo		AS 	Item_Titulo,
    I.Resumen		AS	Item_Resumen,
    I.VecesVisto	AS 	Item_VecesVisto,
    I.Perfil_Id		AS	Perfil_Id,
    I.UrlImagen     AS  Item_UrlImagen,
    P.Nombre		AS	Perfil_Nombre,
    P.UrlImagen		AS	Perfil_UrlImagen,
    P.Nacionalidad	AS	Perfil_Nacionalidad,
    P.Cuenta_Email	AS	Cuenta_Email,
    C.Contrasena	AS	Cuenta_Contrasena
    
    
FROM 
    item		AS I
INNER JOIN perfil AS P
	ON P.Id = I.Perfil_Id
INNER JOIN ubicacion AS U
	ON U.Perfil_Id = I.Perfil_Id
INNER JOIN cuenta AS C
	ON P.Cuenta_Email = C.Email
    WHERE
    I.Disponibilidad = 1 
    AND
    ( 6371 * acos( cos( radians(inUbicacion_Latitude) ) * cos( radians( U.Latitude ) ) * cos( radians( U.Longitude ) - radians(inUbicacion_Longitude) ) + sin( radians(inUbicacion_Latitude) ) * sin( radians( U.Latitude ) ) ) ) <= inRadio AND
    (I.Titulo LIKE  CONCAT("%",inTermino,"%") 		OR
    I.Resumen LIKE  CONCAT("%",inTermino, "%") 	)	
    ORDER BY I.Titulo
    LIMIT
		inOffset, inLimit 
	;
    
    
    
END $$
 