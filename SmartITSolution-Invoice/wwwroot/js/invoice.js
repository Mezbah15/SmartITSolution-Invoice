
    let rowIndex = 1;

    document.getElementById('addRow')
    .addEventListener('click', function () {

        let row = `
    <tr>
        <td>
            <input name="Items[${rowIndex}].Description"
                class="form-control" />
        </td>

        <td>
            <input name="Items[${rowIndex}].Quantity"
                type="number"
                step="1"
                value="1"
                class="form-control qty" />
        </td>

        <td>
            <input name="Items[${rowIndex}].Rate"
                type="number"
                step="1"
                value=""
                class="form-control rate" />
        </td>

        <td>
            <input class="form-control amount"
                readonly />
        </td>

        <td class="text-center">
            <button type="button"
                class="btn btn-danger remove-row">
                X
            </button>
        </td>
    </tr>
    `;

    document
    .getElementById('itemBody')
    .insertAdjacentHTML('beforeend', row);

    rowIndex++;
    });

    document.addEventListener('click', function (e) {

        if (e.target.classList.contains('remove-row')) {

            if (document.querySelectorAll('#itemBody tr').length > 1) {
        e.target.closest('tr').remove();
    calculateTotal();
            }
        }
    });

    document.addEventListener('input', function (e) {

        if (e.target.classList.contains('qty') ||
    e.target.classList.contains('rate')) {

        let row = e.target.closest('tr');

    let qty =
    parseFloat(row.querySelector('.qty').value) || 0;

    let rate =
    parseFloat(row.querySelector('.rate').value) || 0;

    let amount = qty * rate;

    row.querySelector('.amount').value =
    amount.toFixed(2);

    calculateTotal();
        }
    });

    function calculateTotal() {

        let total = 0;

    document.querySelectorAll('.amount')
    .forEach(function (input) {

        total += parseFloat(input.value) || 0;
        });

    document.getElementById('grandTotal')
            .value = total.toFixed(2);
        document.getElementById("inWords").innerText =
            numberToWords(Math.floor(total)) + " Taka Only";
}

document.getElementById('invoiceForm')
    .addEventListener('submit', function () {
        setTimeout(() => this.reset(), 500);
    });