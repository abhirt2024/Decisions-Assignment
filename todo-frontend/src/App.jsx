import {useState, useEffect, useCallback} from "react";
import {todoService} from './services/api';
import './App.css';

function App(){
  const[todos, setTodods] = useState([]);
  const[search, setSearch] = useState('');
  const [categoryFilter, setCategoryFilter] = useState('');
  const[error, setError] = useState(null);

  //Form state
  const[title, setTitle] = useState('');
  const[description, setDescription] = useState('');
  const[priority, setPriority] = useState(1);
  const[category, setCategory] = useState(0);

  //Load Todos
  const fetchTodos = useCallback (async ()=>{
    try{
      setError(null);
      const data = await todoService.getAll(search, categoryFilter);
      setTodods(data);
    }catch(err){
      setError('Could not load tasks, Please ensure the backend is running');
    }
  }, [search, categoryFilter]);

  useEffect(()=>{
    const delayDebounceFn = setTimeout(() => {
      fetchTodos();
    }, 300);
    return ()=> clearTimeout(delayDebounceFn);
  },[fetchTodos]);

  const handleCreate = async(e)=>{
    e.preventDefault();
    try{
      await todoService.create({title, description, priority: Number(priority), category: Number(category)});
      setTitle('');
      setDescription('');
      fetchTodos();
    }catch(err){
      setError('Failed to create task.');
    }
  };

  // Helpers to handle both TitleCase and UPPERCASE from the backend
    const priorityMapRev = { "LOW": 0, "MEDIUM": 1, "HIGH": 2, "Low": 0, "Medium": 1, "High": 2 };
    const categoryMapRev = { "WORK": 0, "PERSONAL": 1, "Work": 0, "Personal": 1 };

    const toggleComplete = async (todo) => {
        try {
            const updatedTodo = { 
                title: todo.title,
                description: todo.description,
                isCompleted: !todo.isCompleted,
                // Use the map, fallback to 1 (Medium) or 0 (Work) if it fails
                priority: priorityMapRev[todo.priority] ?? 1, 
                category: categoryMapRev[todo.category] ?? 0
            };
            await todoService.update(todo.id, updatedTodo);
            fetchTodos();
        } catch (err) {
            console.error("Update failed:", err);
            setError('Failed to update task.');
        }
    };

    const handleDelete = async (id) => {
        try {
            await todoService.delete(id);
            fetchTodos();
        } catch (err) {
            console.error("Delete failed:", err);
            setError('Failed to delete task.');
        }
    };
    
  return(
    <div style={{padding: '20px', maxWidth: '800px', margin: '0 auto'}}>
      <h1>Todo Application</h1>
      {error && <div style={{color: 'red', marginBottom: '10px'}}>{error}</div>}
      <form onSubmit={handleCreate} style={{display: 'flex', gap: '10px', marginBottom: '20px', flexWrap: 'wrap'}}>
        <input required placeholder="Task Title" value={title} onChange={(e)=> setTitle(e.target.value)}/>
        <input placeholder="Description" value={description} onChange={(e)=> setDescription(e.target.value)}/>

        <select value={priority} onChange={(e)=> setPriority(e.target.value)}>
          <option value={0}>Low Priority</option>
          <option value={1}>Medium Priority</option>
          <option value={2}>High Priority</option>
        </select>
        <select value={category} onChange={(e)=> setCategory(e.target.value)}>
          <option value={0}>Work</option>
          <option value={1}>Personal</option>
        </select>

        <button type="submit">Add Task</button>
      </form>

      <div style={{marginBottom: '20px', display: 'flex', gap: '10px', alignItems: 'center'}}>
        <input placeholder="Search tasks..." value={search} onChange={(e)=> setSearch(e.target.value)} style={{padding: '5px', width: '200px'}}/>
        <button onClick={()=> setCategoryFilter('')} style={{fontWeight: categoryFilter === '' ? 'bold': 'normal'}}>
          View All
        </button>
        <button onClick={()=> setCategoryFilter(0)} style={{fontWeight: categoryFilter === 0 ? 'bold': 'normal'}}>
          View Work Todo
        </button>
        <button onClick={()=> setCategoryFilter(1)} style={{fontWeight: categoryFilter === 1 ? 'bold': 'normal'}}>
          View Personal Todo
        </button>
      </div>

      <ul style={{listStyle: 'none', padding: 0}}>
        {todos.map(todo=>(
          <li key={todo.id} style={{
            border: '1px solid #ccc', padding: '10px', marginBottom: '10px',
            display: 'flex', justifyContent: 'space-between', alignItems: 'center',
            opacity: todo.isCompleted?0.6:1
          }}>
            <div>
              <h3 style={{textDecoration: todo.isCompleted?'line-through':'none', margin: '0 0 5px 0'}}>
                {todo.title}
              </h3>
              <p style={{margin: '0 0 5px 0', fontSize: '14px'}}>{todo.description}</p>
              <small>
                <strong>Priority:</strong>{todo.priority} | <strong>Category:</strong>{todo.category}
              </small>
            </div>
            <div style={{display: 'flex', gap: '10px'}}>
              <button onClick={()=>toggleComplete(todo)}>
                {todo.isCompleted?'Mark Incomplete':'Mark Complete'}
              </button>
              <button onClick={()=>handleDelete(todo.id)} style={{color: 'red'}}>
                Delete
              </button>
            </div>
          </li>
        ))}
        {todos.length === 0 && <p> No tasks found.</p>}
      </ul>
    </div>
  )
}
export default App;