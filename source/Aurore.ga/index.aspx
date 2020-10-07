<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="index.aspx.vb" Inherits="Aurore.ga.index" %>

<!DOCTYPE html>
<html>
<head>
	<meta charset="utf-8">
	<meta content="width=device-width, initial-scale=1, shrink-to-fit=no" name="viewport">

	<link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet">
	<link href="https://fonts.googleapis.com/css2?family=Noto+Sans+KR:wght@300&display=swap" rel="stylesheet">

	<script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script> 
	<script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.1/dist/umd/popper.min.js"></script> 
	<script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>

	<title>Aurore.ga - URL를 짧게</title>
</head>
<body>
	<form id="form1" runat="server" action="./">
		<br />
		<main role="main" class="container">
			<div class="shadow p-4 mb-5 bg-white rounded">
				<h2>aurore.ga</h2>
				<p class="lead">aurore.ga는 긴 URL을 짧게 줄여 주는 서비스입니다.</p>
				<label for="lblHint">단축할 URL</label>
				<input runat="server" type="text" class="form-control" id="txtUrl" placeholder="Url을 입력해 주세요.">
				<br />
				<button ID="btnGen" runat="server" class="btn btn-lg btn-primary">생성 »</button>
			</div>
		</main>

		<div class="modal fade" id="pnlModal" tabindex="-1" role="dialog" aria-labelledby="pnlModalLabel" aria-hidden="true">
		  <div class="modal-dialog" role="document">
			<div class="modal-content">
			  <div class="modal-header">
				<h5 class="modal-title" id="pnlModalLabel">생성 완료!</h5>
				<button type="button" class="close" data-dismiss="modal" aria-label="Close">
				  <span aria-hidden="true">&times;</span>
				</button>
			  </div>
			  <div class="modal-body">
				  <p id="txtDes"></p>
			  </div>
			  <div class="modal-footer">
				<button type="button" class="btn btn-primary" data-dismiss="modal">OK</button>
			  </div>
			</div>
		  </div>
		</div>

		<script>
            function run(text1) {
                $('#txtDes').text(text1)
                $('#pnlModal').modal('show')
            }
        </script>
	</form>

	<style>
		body{
			background-color: #f5f5f5;
			font-family: 'Noto Sans KR', sans-serif;
		}
	</style>
</body>
</html>