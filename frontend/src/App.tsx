import './App.css';

function App() {
  return (
    <div style={{ padding: '2rem' }}>
      <h1>🎯 eBay Fullstack - Vite + React</h1>
      <p style={{ color: '#888' }}>Backend: ASP.NET Core | Frontend: Vite + React + TypeScript</p>

      <div style={{ marginTop: '2rem', padding: '1.5rem', background: '#f0f9ff', border: '1px solid #3b82f6', borderRadius: '8px' }}>
        <h2>📚 Nhiệm vụ học tập:</h2>
        <ol style={{ textAlign: 'left', lineHeight: '2' }}>
          <li>Chạy backend: <code>cd backend && dotnet run</code></li>
          <li>Import <code>productApi</code> từ <code>src/api/products</code></li>
          <li>Dùng <code>useState</code> để lưu products, loading, error</li>
          <li>Dùng <code>useEffect</code> để gọi API khi component mount</li>
          <li>Hiển thị danh sách products</li>
        </ol>
      </div>

      <div style={{ marginTop: '1.5rem', padding: '1.5rem', background: '#fffbeb', border: '1px solid #f59e0b', borderRadius: '8px' }}>
        <h2>💡 Gợi ý:</h2>
        <ul style={{ textAlign: 'left', lineHeight: '2' }}>
          <li>Xem file <code>src/api/products.ts</code> để biết có API nào</li>
          <li>Types đã có sẵn trong <code>src/types/index.ts</code></li>
          <li>API Client đã setup trong <code>src/api/client.ts</code></li>
          <li>Backend Swagger: <a href="http://localhost:5000" target="_blank">http://localhost:5000</a></li>
        </ul>
      </div>
    </div>
  );
}

export default App;
