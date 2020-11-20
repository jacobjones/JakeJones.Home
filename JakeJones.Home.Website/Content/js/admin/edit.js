ready(function () {
	if (pageIs("blog--edit")) {
		tinymce.init({
			selector: '#Content',
			autoresize_min_height: 500,
			plugins: 'codesample autosave preview searchreplace visualchars image link media fullscreen code codesample table hr pagebreak autoresize nonbreaking anchor insertdatetime advlist lists textcolor wordcount imagetools colorpicker',
			menubar: "edit view format insert table",
			toolbar1: 'codesample | formatselect | bold italic blockquote forecolor backcolor | imageupload link | alignleft aligncenter alignright  | numlist bullist outdent indent | fullscreen',
			selection_toolbar: 'bold italic | quicklink h2 h3 blockquote',
			autoresize_bottom_margin: 0,
			paste_data_images: false,
			image_advtab: true,
			//file_picker_types: 'image',
			relative_urls: false,
			convert_urls: false,
			branding: false,
			images_upload_url: "/blog/image"
		});
	}
});