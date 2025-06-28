import 'dart:convert';
import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import 'package:my_coma/Models/API.dart';
import '../../Models/clsProduct.dart';
import '../../Models/clsOrder.dart';

class ProductsScreen extends StatefulWidget {
  final int tableId;

  ProductsScreen({required this.tableId});

  @override
  _ProductsScreenState createState() => _ProductsScreenState();
}

class _ProductsScreenState extends State<ProductsScreen> {
  List<Product> products = [];
  List<Order> orders = [];
  List<Product> filteredProducts = []; 
  bool isLoading = false;
  bool hasOrders = false;
  bool hasProducts = false;
  bool isOpen = false;
  String searchQuery = '';
  String errorMessage = '';

  @override
  void initState() {
    super.initState();
    loadData();
  }

  Future<void> loadData() async {
    setState(() {
      isLoading = true;
    });
    await fetchProducts();
    await fetchOrders();
    setState(() {
      isLoading = false;
    });
  }

  Future<void> fetchProducts() async {
    final response = await http.get(Uri.parse('${clsAPI.baseURL}/${clsAPI.PRODUCTS}'));

    if (response.statusCode == 200) {
      List jsonResponse = json.decode(response.body);
      setState(() {
        products = jsonResponse.map((data) => Product.fromJson(data)).toList();
        filteredProducts = products; 
        hasProducts = true;
        errorMessage = "";
      });
    }else if(response.statusCode == 404){
      setState(() {
        errorMessage = "Hiç bir ürün bulunmamaktadır.";
      });
    } 
    else {
      setState(() {
        hasProducts = false;
        errorMessage = "Ürünler yüklenirken hata oluştu.";
      });
    }
  }

  Future<void> fetchOrders() async {
    final response = await http.get(Uri.parse('${clsAPI.baseURL}/${clsAPI.TABLE_ORDERS}/${widget.tableId}'));

    if (response.statusCode == 200) {
      List jsonResponse = json.decode(response.body);
      if (jsonResponse.isNotEmpty) {
        setState(() {
          hasOrders = true;
          orders = jsonResponse.map((data) => Order.fromJson(data)).toList();
        });
      } else {
        setState(() {
          hasOrders = false;
        });
      }
    }else if(response.statusCode == 404){
      setState(() {
        errorMessage = "Bu masaya ait sipariş yoktur.";
      });
    } 
    else {
      setState(() {
        errorMessage = "Siparişleri yüklerken hata oluştu.";
      });
    }
  }

  Future<void> addOrder(int productId) async {
    final response = await http.post(
      Uri.parse('${clsAPI.baseURL}/${clsAPI.ORDERS}'),
      headers: <String, String>{
        'Content-Type': 'application/json; charset=UTF-8',
      },
      body: jsonEncode(<String, dynamic>{
        'id': 0,
        'tableId': widget.tableId,
        'productId': productId,
        'status': 'Siparis Edildi',
      }),
    );

    if (response.statusCode == 200) {
      final newOrder = Order.fromJson(json.decode(response.body));
      setState(() {
        errorMessage = "";
        orders.add(newOrder);
        hasOrders = true;
      });
      
      await loadData();
    } else {
        setState(() {
        errorMessage = "Sipariş eklenirken hata oluştu.";
      });
    }
  }

  Future<void> cancelOrder(int orderId) async {
    final response = await http.delete(Uri.parse('${clsAPI.baseURL}/${clsAPI.ORDERS}/$orderId/${widget.tableId}'));

    if (response.statusCode == 200) {
      setState(() async {
        errorMessage = "";
        orders.removeWhere((order) => order.id == orderId);
        if (orders.isEmpty) {
          hasOrders = false;
          errorMessage = "Bu masaya ait sipariş yoktur.";
        }
        // Siparişler yenilenecek
        await loadData();
      });
    }else if(response.statusCode == 404){
      setState(() {
        isOpen = false;
        hasOrders = false;
        errorMessage = "Bu masaya ait sipariş yoktur.";
      });
    } 
    else {
      setState(() {
        errorMessage = "Sipariş iptal edilirken hata oluştu.";
      });
    }
  }

  void filterProducts(String query) {
    setState(() {
      searchQuery = query;
      filteredProducts = products
          .where((product) => product.name.toLowerCase().contains(query.toLowerCase()))
          .toList();
    });
  }

@override
Widget build(BuildContext context) {
  return Scaffold(
    appBar: AppBar(
      title: Text('Ürünler'),
      bottom: PreferredSize(
        preferredSize: Size.fromHeight(50.0),
        child: Padding(
          padding: const EdgeInsets.all(8.0),
          child: TextField(
            onChanged: filterProducts,
            decoration: InputDecoration(
              hintText: 'Ürün Ara...',
              prefixIcon: Icon(Icons.search),
              filled: true,
              fillColor: Colors.white,
              border: OutlineInputBorder(
                borderRadius: BorderRadius.circular(10),
              ),
            ),
          ),
        ),
      ),
    ),
    body: isLoading
        ? Center(child: CircularProgressIndicator())
        : Column(
            children: [
              if (errorMessage.isNotEmpty)
                Padding(
                  padding: const EdgeInsets.all(8.0),
                  child: Text(
                    errorMessage,
                    style: TextStyle(color: Colors.red, fontWeight: FontWeight.bold),
                  ),
                ),
              // Siparişler başlığı
              // Siparişler başlığı
              if (hasOrders && orders.isNotEmpty) // Siparişler varsa göster
                Padding(
                  padding: const EdgeInsets.symmetric(vertical: 12.0), // Daha fazla boşluk
                  child: GestureDetector(
                    onTap: () {
                      setState(() {
                        isOpen = !isOpen; // Siparişlerin görünürlüğünü değiştir
                      });
                    },
                    child: Container(
                      padding: EdgeInsets.all(16),
                      color: Colors.blue[400], // Daha canlı bir mavi tonu
                      child: Row(
                        mainAxisAlignment: MainAxisAlignment.spaceBetween,
                        children: [
                          Text('Siparişler (${orders.length})', style: TextStyle(fontSize: 20, fontWeight: FontWeight.bold)), // Font boyutu artırıldı
                          Icon(isOpen ? Icons.arrow_drop_up : Icons.arrow_drop_down),
                        ],
                      ),
                    ),
                  ),
                ),
              if (isOpen) // Siparişler görünüyorsa göster
                Expanded(
                  child: Container(
                    color: Colors.grey[200],
                    padding: const EdgeInsets.all(8.0),
                    child: ListView(
                      children: orders.map((order) {
                        final product = products.firstWhere((prod) => prod.id == order.productId);
                        return Card(
                          shape: RoundedRectangleBorder(
                            borderRadius: BorderRadius.circular(10), // Kenar yumuşatma
                          ),
                          color: Colors.lightBlue[50], // Daha açık bir mavi tonu
                          margin: EdgeInsets.symmetric(vertical: 4),
                          child: ListTile(
                            title: Text(product.name),
                            subtitle: Text('Fiyat: ${product.price} - Durum: ${order.status}'),
                            trailing: order.status != 'Teslim Edildi'
                                ? IconButton(
                                    icon: Icon(Icons.remove),
                                    onPressed: () => cancelOrder(order.id),
                                  )
                                : null,
                          ),
                        );
                      }).toList(),
                    ),
                  ),
                ),

              if (hasProducts && filteredProducts.isEmpty)
                Expanded(
                  child: Center(
                    child: Text(
                      'Ürün bulunamadı.',
                      style: TextStyle(fontSize: 16, fontWeight: FontWeight.bold),
                    ),
                  ),
                ),
              Expanded(
                child: ListView.builder(
                  itemCount: filteredProducts.length,
                  itemBuilder: (context, index) {
                    final product = filteredProducts[index];
                    return Card(
                      margin: EdgeInsets.all(10),
                      elevation: 5,
                      child: ListTile(
                        title: Text(product.name, style: TextStyle(fontSize: 16, fontWeight: FontWeight.bold)),
                        subtitle: Text('Fiyat: ${product.price} - Durum: ${product.status}'),
                        trailing: product.status == 'Var'
                            ? IconButton(
                                icon: Icon(Icons.add),
                                onPressed: () => addOrder(product.id),
                              )
                            : null,
                      ),
                    );
                  },
                ),
              ),
            ],
          ),
  );
}
}
