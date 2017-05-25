<%@ Page Title="" Language="C#"  AutoEventWireup="true" CodeBehind="nuevaFecha.aspx.cs" Inherits="Prueba.Presentacion.nuevaFecha" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <title></title>
    <!-- estilo de controles de la aplicacion -->
    <link type="text/css" rel="stylesheet" href="Content/bootstrap.css" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css">
    <script src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
    <script>
        $(function () {
            $.datepicker.setDefaults($.datepicker.regional["es"]);
            $("#datepicker").datepicker({
                dateFormat: 'yy-mm-dd',
                onSelect: function(datetext){
                var d = new Date(); // for now
                var strHora = '' + d.getHours();
                
                var strMinutos = '' + d.getMinutes();
                if (strMinutos.length == 1) {
                    strMinutos = '0' + strMinutos;
                }
                var strSegundos = '' + d.getSeconds();
                if (strSegundos.length == 1) {
                    strSegundos = '0' + strSegundos;
                }
                var date_obj_am_pm = "";
                if (d.getHours() > 11) {
                    strHora = strHora - 12;
                    date_obj_am_pm = " PM";
                } else {
                    date_obj_am_pm = " AM";
                }
                datetext = datetext + " " + d.getHours() + ":" + strMinutos + ":" + strSegundos + "" + date_obj_am_pm;
                $('#datepicker').val(datetext);
            },
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="container-fluid">
                <div class="row">
                    <br />
                    <h2>Nueva Fecha de Termino:</h2>
                    <div class="col-md-2">
                        <div class="form-group">
                            <label for="exampleInputFecha">Fecha:</label>
                            <asp:TextBox ID="datepicker" runat="server" class="form-control" name="nuevaFechaF"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:HiddenField ID="idOrden" runat="server" />
                            <asp:HiddenField ID="timepickerIni" runat="server"/>
                            <asp:HiddenField ID="nombreOrden" runat="server" />
                            <asp:HiddenField ID="statusOrden" runat="server"/>
                        </div>
                        <asp:Button ID="ButtonOK" runat="server" class="btn btn-success" OnClick="ButtonOK_Click" Text="OK" />
                        <asp:Button ID="ButtonCancel" runat="server" class="btn btn-danger" OnClick="ButtonCancel_Click" Text="Cancelar"/>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript">
        function cerrar(result) { parent.DayPilot.ModalStatic.close(result); }
    </script>
    <script>
        function close(result) {
            if (parent && parent.DayPilot && parent.DayPilot.ModalStatic) {
                parent.DayPilot.ModalStatic.close(result);
            }
        }
</script>
</body>
</html>