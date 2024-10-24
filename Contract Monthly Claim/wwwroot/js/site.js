function showRejectionInput() {
    document.getElementById('rejectionReasonInput').style.display = 'flex';
    document.getElementById('rejectionReasonInput').style.flexDirection = 'column';
    document.getElementById('approve-claim').style.display = 'none';
    document.getElementById('reject-claim').style.display = 'none';
    document.getElementById('submitRejection').style.display = 'block';
}