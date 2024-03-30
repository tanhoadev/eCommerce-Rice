// =============  Data Table - (Start) ================= //

$(document).ready(function(){
    
    var table = $('#example').DataTable({
        
        buttons:['copy', 'csv', 'excel', 'pdf', 'print']
        
    });
    
    
    table.buttons().container()
    .appendTo('#example_wrapper .col-md-6:eq(0)');

});
function printModalContent(x) {
    var modalContent = document.querySelector(x).outerHTML;
    var printWindow = window.open('', '_blank');
    printWindow.document.write('<html><head><title>Modal Content</title>');
    printWindow.document.write('<link href="/user/css/style.css" rel="stylesheet">');
    printWindow.document.write('</head><body>');
    printWindow.document.write(modalContent);
    printWindow.document.write('</body></html>');
    printWindow.document.close();
    printWindow.print();
}
// =============  Data Table - (End) ================= //
