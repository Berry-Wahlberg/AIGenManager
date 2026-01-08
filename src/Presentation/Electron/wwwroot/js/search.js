// Search functionality for AIGenManager Electron app

// DOM elements
const searchInput = document.getElementById('search-input');
const searchBtn = document.getElementById('search-btn');
const filterBtn = document.getElementById('filter-btn');
const filtersPanel = document.getElementById('filters-panel');
const closeFiltersBtn = document.getElementById('close-filters-btn');
const resultsCount = document.getElementById('results-count');
const searchTime = document.getElementById('search-time');
const gridViewBtn = document.getElementById('grid-view-btn');
const listViewBtn = document.getElementById('list-view-btn');
const thumbnailSizeSlider = document.getElementById('thumbnail-size-slider');
const thumbnailSizeValue = document.getElementById('thumbnail-size-value');
const thumbnailGrid = document.getElementById('thumbnail-grid');
const previewPanel = document.getElementById('preview-panel');
const closePreviewBtn = document.getElementById('close-preview-btn');

// State variables
let currentViewMode = 'grid';
let currentThumbnailSize = parseInt(thumbnailSizeSlider.value);
let currentSearchQuery = '';
let selectedThumbnail = null;

// Initialize search page
function initSearchPage() {
    console.log('Initializing search page...');
    
    // Load initial data
    loadInitialImages();
    
    // Event listeners
    searchInput.addEventListener('input', handleSearchInput);
    searchInput.addEventListener('keypress', handleSearchKeyPress);
    searchBtn.addEventListener('click', performSearch);
    filterBtn.addEventListener('click', toggleFiltersPanel);
    closeFiltersBtn.addEventListener('click', closeFiltersPanel);
    gridViewBtn.addEventListener('click', () => setViewMode('grid'));
    listViewBtn.addEventListener('click', () => setViewMode('list'));
    thumbnailSizeSlider.addEventListener('input', handleThumbnailSizeChange);
    closePreviewBtn.addEventListener('click', closePreviewPanel);
    
    // Click outside to close panels
    document.addEventListener('click', handleDocumentClick);
    
    console.log('Search page initialized.');
}

// Load initial images
async function loadInitialImages() {
    showLoadingState();
    
    try {
        // Simulate loading images (replace with actual API call later)
        await new Promise(resolve => setTimeout(resolve, 1000));
        
        // Mock data - replace with actual data from backend
        const mockImages = generateMockImages(20);
        
        // Update UI with results
        updateSearchResults(mockImages);
        updateResultsInfo(mockImages.length, 1000);
        
        // Render thumbnails
        renderThumbnails(mockImages);
    } catch (error) {
        console.error('Error loading images:', error);
        showErrorState('Failed to load images. Please try again later.');
    }
}

// Generate mock images for testing
function generateMockImages(count) {
    const images = [];
    for (let i = 0; i < count; i++) {
        images.push({
            id: i + 1,
            path: `image_${i + 1}.png`,
            filename: `image_${i + 1}.png`,
            thumbnailUrl: `https://picsum.photos/seed/image${i + 1}/256/256`,
            width: 512,
            height: 512,
            createdAt: new Date(Date.now() - Math.random() * 30 * 24 * 60 * 60 * 1000),
            tags: ['tag1', 'tag2', 'tag3'],
            prompt: `Generated image ${i + 1} with AI`,
            metadata: {
                model: 'Stable Diffusion 1.5',
                steps: Math.floor(Math.random() * 50) + 10,
                cfg: (Math.random() * 5 + 5).toFixed(1),
                sampler: 'Euler a',
                seed: Math.floor(Math.random() * 1000000)
            }
        });
    }
    return images;
}

// Handle search input changes
function handleSearchInput(e) {
    currentSearchQuery = e.target.value;
    // Implement debounce for real-time search
    clearTimeout(window.searchTimeout);
    window.searchTimeout = setTimeout(performSearch, 500);
}

// Handle search key press (Enter)
function handleSearchKeyPress(e) {
    if (e.key === 'Enter') {
        performSearch();
    }
}

// Perform search
async function performSearch() {
    console.log('Performing search for:', currentSearchQuery);
    showLoadingState();
    
    try {
        const startTime = performance.now();
        
        // Simulate search delay
        await new Promise(resolve => setTimeout(resolve, 500));
        
        // Mock search results based on query
        let mockImages = generateMockImages(20);
        if (currentSearchQuery) {
            mockImages = mockImages.filter(img => 
                img.filename.toLowerCase().includes(currentSearchQuery.toLowerCase()) ||
                img.prompt.toLowerCase().includes(currentSearchQuery.toLowerCase())
            );
        }
        
        const endTime = performance.now();
        const searchDuration = Math.round(endTime - startTime);
        
        // Update UI
        updateSearchResults(mockImages);
        updateResultsInfo(mockImages.length, searchDuration);
        renderThumbnails(mockImages);
    } catch (error) {
        console.error('Search error:', error);
        showErrorState('Search failed. Please try again.');
    }
}

// Toggle filters panel
function toggleFiltersPanel() {
    filtersPanel.classList.toggle('open');
}

// Close filters panel
function closeFiltersPanel() {
    filtersPanel.classList.remove('open');
}

// Close preview panel
function closePreviewPanel() {
    previewPanel.classList.remove('open');
    selectedThumbnail = null;
}

// Set view mode (grid/list)
function setViewMode(mode) {
    currentViewMode = mode;
    
    // Update UI
    if (mode === 'grid') {
        gridViewBtn.classList.add('active');
        listViewBtn.classList.remove('active');
        thumbnailGrid.classList.remove('list-view');
    } else {
        listViewBtn.classList.add('active');
        gridViewBtn.classList.remove('active');
        thumbnailGrid.classList.add('list-view');
    }
    
    // Update thumbnail size based on view mode
    updateThumbnailSize();
}

// Handle thumbnail size change
function handleThumbnailSizeChange(e) {
    currentThumbnailSize = parseInt(e.target.value);
    thumbnailSizeValue.textContent = `${currentThumbnailSize}px`;
    updateThumbnailSize();
}

// Update thumbnail size
function updateThumbnailSize() {
    if (currentViewMode === 'grid') {
        thumbnailGrid.style.gridTemplateColumns = `repeat(auto-fill, minmax(${currentThumbnailSize}px, 1fr))`;
    }
}

// Show loading state
function showLoadingState() {
    thumbnailGrid.innerHTML = `
        <div class="loading-state">
            <div class="loading-spinner"></div>
            <p>Loading images...</p>
        </div>
    `;
}

// Show error state
function showErrorState(message) {
    thumbnailGrid.innerHTML = `
        <div class="loading-state">
            <p style="color: #e74c3c; font-weight: 600;">⚠️ Error</p>
            <p>${message}</p>
            <button onclick="loadInitialImages()" class="retry-btn" style="margin-top: 1rem; padding: 0.5rem 1rem; background-color: #3498db; color: white; border: none; border-radius: 4px; cursor: pointer;">
                Retry
            </button>
        </div>
    `;
}

// Render thumbnails
function renderThumbnails(images) {
    if (images.length === 0) {
        thumbnailGrid.innerHTML = `
            <div class="loading-state">
                <p>No images found.</p>
                <p>Try adjusting your search criteria.</p>
            </div>
        `;
        return;
    }
    
    // Generate thumbnail HTML
    const thumbnailsHTML = images.map(image => `
        <div class="thumbnail-item" data-image-id="${image.id}" onclick="selectThumbnail(${image.id}, this)">
            <img 
                src="${image.thumbnailUrl}" 
                alt="${image.filename}" 
                class="thumbnail-image"
                loading="lazy"
            />
            <div class="thumbnail-overlay">
                <span>${image.filename}</span>
            </div>
        </div>
    `).join('');
    
    thumbnailGrid.innerHTML = thumbnailsHTML;
}

// Select thumbnail
function selectThumbnail(imageId, element) {
    // Deselect previous selection
    if (selectedThumbnail) {
        selectedThumbnail.classList.remove('selected');
    }
    
    // Select new thumbnail
    selectedThumbnail = element;
    selectedThumbnail.classList.add('selected');
    
    // Show preview panel (mock implementation)
    showPreviewPanel(imageId);
}

// Show preview panel
function showPreviewPanel(imageId) {
    console.log('Showing preview for image:', imageId);
    previewPanel.classList.add('open');
    
    // Mock preview content
    const previewContent = `
        <div class="preview-header">
            <h4>Image ${imageId}</h4>
        </div>
        <div class="preview-image-container">
            <img src="https://picsum.photos/seed/image${imageId}/800/600" alt="Preview" class="preview-image" style="width: 100%; max-height: 400px; object-fit: contain; border-radius: 8px; margin-bottom: 1rem;">
        </div>
        <div class="preview-info">
            <div class="preview-section">
                <h5>Metadata</h5>
                <div class="metadata-grid">
                    <div class="metadata-item">
                        <span class="metadata-label">Filename:</span>
                        <span class="metadata-value">image_${imageId}.png</span>
                    </div>
                    <div class="metadata-item">
                        <span class="metadata-label">Size:</span>
                        <span class="metadata-value">512 × 512</span>
                    </div>
                    <div class="metadata-item">
                        <span class="metadata-label">Model:</span>
                        <span class="metadata-value">Stable Diffusion 1.5</span>
                    </div>
                    <div class="metadata-item">
                        <span class="metadata-label">Steps:</span>
                        <span class="metadata-value">30</span>
                    </div>
                    <div class="metadata-item">
                        <span class="metadata-label">CFG Scale:</span>
                        <span class="metadata-value">7.5</span>
                    </div>
                    <div class="metadata-item">
                        <span class="metadata-label">Sampler:</span>
                        <span class="metadata-value">Euler a</span>
                    </div>
                    <div class="metadata-item">
                        <span class="metadata-label">Seed:</span>
                        <span class="metadata-value">${Math.floor(Math.random() * 1000000)}</span>
                    </div>
                </div>
            </div>
            <div class="preview-section">
                <h5>Prompt</h5>
                <div class="prompt-content" style="background-color: #f5f5f5; padding: 1rem; border-radius: 8px; font-family: monospace; white-space: pre-wrap; font-size: 0.9rem;">
                    Generated image ${imageId} with AI
                    <br><br>
                    <strong>Positive:</strong> beautiful scenery, mountain landscape, lake, sunset, highly detailed, 4k
                    <br><br>
                    <strong>Negative:</strong> blurry, low quality, ugly, distorted
                </div>
            </div>
        </div>
    `;
    
    document.querySelector('.preview-content').innerHTML = previewContent;
}

// Update search results info
function updateSearchResultsInfo(count, duration) {
    resultsCount.textContent = `${count} ${count === 1 ? 'image' : 'images'}`;
    searchTime.textContent = `(${duration} ms)`;
}

// Update search results
function updateSearchResults(images) {
    // This would typically update the internal state with search results
    console.log('Updated search results:', images.length, 'images');
}

// Update results info
function updateResultsInfo(count, duration) {
    resultsCount.textContent = `${count} ${count === 1 ? 'image' : 'images'}`;
    searchTime.textContent = `(${duration} ms)`;
}

// Handle document click to close panels
function handleDocumentClick(e) {
    // Close filters panel if clicking outside
    if (!filtersPanel.contains(e.target) && e.target !== filterBtn) {
        closeFiltersPanel();
    }
    
    // Close preview panel if clicking outside (and not on thumbnail)
    if (!previewPanel.contains(e.target) && e.target !== closePreviewBtn && !e.target.closest('.thumbnail-item')) {
        closePreviewPanel();
    }
}

// Initialize when DOM is loaded
document.addEventListener('DOMContentLoaded', initSearchPage);
