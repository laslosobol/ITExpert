class FilmCategories {
    constructor(selectElementId, url, selectedCategories) {
        this.selectElementId = selectElementId;
        this.url = url;
        this.selectedCategories = selectedCategories;
        this.loadCategories();
    }

    loadCategories() {
        fetch(this.url)
            .then(response => response.json())
            .then(categories => {
                this.categories = categories;
                this.renderCategories();
                this.initSelect2();
            })
            .catch(error => console.error('Error:', error));
    }

    renderCategories() {
        const selectElement = document.getElementById(this.selectElementId);
        selectElement.innerHTML = '';
        this.categories.forEach(category => {
            const option = document.createElement('option');
            option.value = category.id;
            option.textContent = category.name;
            selectElement.appendChild(option);
        });
    }

    initSelect2() {
        const selectElement = $(`#${this.selectElementId}`);
        selectElement.select2();
        this.selectedCategories.forEach(categoryId => {
            selectElement.find(`option[value="${categoryId}"]`).prop('selected', true);
        });
        selectElement.trigger('change');
    }
}