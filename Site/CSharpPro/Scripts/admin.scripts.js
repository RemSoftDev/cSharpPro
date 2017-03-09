$(function () {
    tinymce.init({
        selector: 'textarea.tinymce',
        plugins: [
        "advlist autolink lists link image charmap print preview anchor",
        "searchreplace visualblocks code fullscreen",
        "insertdatetime media table contextmenu paste imagetools"
        ],
        toolbar: "insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image",
        style_formats: [
            {
                title: 'Headers', items: [
                { title: 'h1', block: 'h1' },
                { title: 'h2', block: 'h2' },
                { title: 'h3', block: 'h3' },
                { title: 'h4', block: 'h4' },
                { title: 'h5', block: 'h5' },
                { title: 'h6', block: 'h6' }
                ]
            },
            {
                title: 'Blocks', items: [
                { title: 'p', block: 'p' },
                { title: 'div', block: 'div' },
                { title: 'pre', block: 'pre' }
                ]
            },
            {
                title: 'Containers', items: [
                { title: 'section', block: 'section', wrapper: true, merge_siblings: false },
                { title: 'article', block: 'article', wrapper: true, merge_siblings: false },
                { title: 'blockquote', block: 'blockquote', wrapper: true },
                { title: 'hgroup', block: 'hgroup', wrapper: true },
                { title: 'aside', block: 'aside', wrapper: true },
                { title: 'figure', block: 'figure', wrapper: true }
                ]
            },
            {
                title: 'Adaptive Blocks', items: [
                  { title: 'row', block: 'div', classes: 'row', merge_siblings: false, wrapper: true },
                  { title: 'col 1', block: 'div', classes: 'col-sm-1', merge_siblings: false, wrapper: true },
                  { title: 'col 2', block: 'div', classes: 'col-sm-2', merge_siblings: false, wrapper: true },
                  { title: 'col 3', block: 'div', classes: 'col-sm-3', merge_siblings: false, wrapper: true },
                  { title: 'col 4', block: 'div', classes: 'col-sm-4', merge_siblings: false, wrapper: true },
                  { title: 'col 5', block: 'div', classes: 'col-sm-5', merge_siblings: false, wrapper: true },
                  { title: 'col 6', block: 'div', classes: 'col-sm-6', merge_siblings: false, wrapper: true },
                  { title: 'col 7', block: 'div', classes: 'col-sm-7', merge_siblings: false, wrapper: true },
                  { title: 'col 8', block: 'div', classes: 'col-sm-8', merge_siblings: false, wrapper: true },
                  { title: 'col 9', block: 'div', classes: 'col-sm-9', merge_siblings: false, wrapper: true },
                  { title: 'col 10', block: 'div', classes: 'col-sm-10', merge_siblings: false, wrapper: true },
                  { title: 'col 11', block: 'div', classes: 'col-sm-11', merge_siblings: false, wrapper: true },
                  { title: 'col 12', block: 'div', classes: 'col-sm-12', merge_siblings: false, wrapper: true },
                ]
            }
        ],
        visualblocks_default_state: true,
        end_container_on_empty_block: true
    });
});