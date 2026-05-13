const BASE_URL = 'http://localhost:5000/api/v1/todos';
export const todoService = {
    
    getAll: async (search = '', category = '') => {
        const params = new URLSearchParams();
        if (search) params.append('search', search);
        if (category !== '') params.append('category', category);
        
        // Clean up the URL string
        const queryString = params.toString();
        const finalUrl = queryString ? `${BASE_URL}?${queryString}` : BASE_URL;
        

        try {
            const response = await fetch(finalUrl);
        
            if (!response.ok) {
                const errorText = await response.text();
                console.error("3. Backend error details:", errorText);
                throw new Error('Failed to fetch todos');
            }
            
            return await response.json();
        } catch (error) {
            console.error("Fetch completely failed:", error);
            throw error;
        }
    },

    create: async (todo) => {
        const response = await fetch(BASE_URL, {
            method: 'POST',
            headers: {'Content-Type': 'application/json'},
            body: JSON.stringify(todo)
        });
        if(!response.ok) throw new Error('Failed to create todo');
        return response.json();
    },

    update: async (id, todo) => {
        const response = await fetch(`${BASE_URL}/${id}`,{
            method: 'PUT',
            headers: {'Content-Type': 'application/json'},
            body: JSON.stringify(todo)
        });
        if(!response.ok) throw new Error('Failed to update todo');
    },

    delete: async (id) => {
        const response = await fetch(`${BASE_URL}/${id}`,{
            method: 'DELETE'
        });
        if(!response.ok) throw new Error('Failed to delete todo');
    }
}; 