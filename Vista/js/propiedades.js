$(document).ready(function() 
{
  var iCnt = 0;

  // Crear un elemento div añadiendo estilos CSS
  var container = $(document.getElementById("main"));

  $('#btAdd').click(function() 
  {
    if (iCnt <= 5) 
    {
 
      iCnt = iCnt + 1;
       
      // Añadir caja de texto.
      $(container).append('<div class="form-group">');
      $(container).append('<label id="kl' + iCnt + '" class="control-label col-xs-3">Nombre</label>');
      $(container).append('<div class="col-xs-9 col-sm-9">');
      $(container).append('<input type="text" class="input" id="k' + iCnt + '">');
      $(container).append('</div></div>');


      $(container).append('<div class="form-group">');
      $(container).append('<label id="vl' + iCnt + '" class="control-label col-xs-3">Valor</label>');
      $(container).append('<div class="col-xs-9 col-sm-9">');
      $(container).append('<input type="text" class="input" id="v' + iCnt + '">');
      $(container).append('</div></div>');
       
      /*if (iCnt == 1) {
       
      var divSubmit = $(document.getElementById('div'));
      $(divSubmit).append('<input type=button class="bt" onclick="GetTextValue()"' +
      'id=btSubmit value=Enviar />');
     
      }*/
      $('#main').after(container);//, divSubmit);
    }
    else 
    { //se establece un limite para añadir elementos, 5 es el limite
      $(container).append('<label id="labelLimite" class="text-center">Limite Alcanzado</label>');
      $('#btAdd').attr('class', 'bt-disable');
      $('#btAdd').attr('disabled', 'disabled');
    }
  });

  $('#btRemove').click(function() 
  {
    if (iCnt != 0) { 
      $('#kl' + iCnt).remove();
      $('#vl' + iCnt).remove();
      $('#k' + iCnt).remove();
      $('#v' + iCnt).remove();
      iCnt = iCnt - 1;
     }
    if (iCnt == 0)
    {
      $(container).empty();
    }
  });
});
 
// Obtiene los valores de los textbox al dar click en el boton "Enviar"
var divValue, values = '';
 
function GetTextValue() 
{
  $(divValue).empty();
  $(divValue).remove(); values = '';
   
  $('#key').each(function() {
  divValue = $(document.createElement('div')).css({
  padding:'5px', width:'200px'
  });
  values += this.value + '<br />'
  });
   
  $(divValue).append('<p><b>Tus valores añadidos</b></p>' + values);
  $('body').append(divValue);
}