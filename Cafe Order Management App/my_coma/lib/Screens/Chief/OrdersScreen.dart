import 'dart:convert';
import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import 'package:my_coma/Models/API.dart';
import '../../Models/clsOrders.dart';

class OrdersScreen extends StatefulWidget {
  @override
  _OrdersScreenState createState() => _OrdersScreenState();
}

class _OrdersScreenState extends State<OrdersScreen> {
  List<clsOrders> orders = [];
  bool isLoading = false; // Yüklenme durumu
  bool hasOrders = false; // Sipariş olup olmadığını kontrol eder
  String message = "";
  @override
  void initState() {
    super.initState();
    loadData(); // Siparişleri API'dan çeken fonksiyon
  }

  // Yükleme işlemi ve API'den veri çekme
  Future<void> loadData() async {
    setState(() {
      isLoading = true; // Yükleme başlıyor
    });
    await fetchOrders(); // Siparişleri yükle
    setState(() {
      isLoading = false; // Yükleme bitti
    });
  }

  // API'dan siparişleri çekme
  Future<void> fetchOrders() async {
    try {
      final response = await http.get(Uri.parse('${clsAPI.baseURL}/${clsAPI.ALL_ORDERS}'));

      if (response.statusCode == 200) {
        List jsonResponse = json.decode(response.body);
        setState(() {
          orders = jsonResponse.map((data) => clsOrders.fromJson(data)).toList();
          hasOrders = orders.isNotEmpty; 
        });
      }else if(response.statusCode == 404)
      {
        setState(() {
          message = "Sipariş Yok.";
        });
      } 
      else {
        setState(() {
          hasOrders = false; 
          message = "Siparişler yüklenirken hata oluştu.";
        });
      }
    } catch (error) {
      setState(() {
        hasOrders = false; 
      });
    } finally {
      setState(() {
        isLoading = false; 
      });
    }
  }

  // Siparişin durumunu güncelleyen fonksiyon
  Future<void> updateOrderStatus(int orderId, String status) async {
    final response = await http.put(
      Uri.parse('${clsAPI.baseURL}/${clsAPI.UPDATE_STATUS}/$orderId/$status'),
    );

    if (response.statusCode == 200) {
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(content: Text('Sipariş Teslim edildi.'),backgroundColor: Colors.green),
      );
      await loadData(); // Durum güncellenince siparişleri tekrar yükle
    } else {
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(content: Text('Sipariş teslim edilemedi.'),backgroundColor: Colors.red,),
      );
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
    title: Text('Şef Sipariş Ekranı'),
    actions: [
      IconButton(
        icon: Icon(Icons.refresh),
        onPressed: () async {
          // Buraya fonksiyonu çağırın
          await loadData(); // Bu, belirtmek istediğiniz fonksiyon
        },
      ),
    ],
  ),
      body: isLoading
          ? Center(child: CircularProgressIndicator()) // Yükleniyor göstergesi
          : hasOrders
              ? ListView.builder(
                  itemCount: orders.length,
                  itemBuilder: (context, index) {
                    return Card(
                      margin: EdgeInsets.all(10),
                      child: ListTile(
                        leading: Icon(Icons.local_dining, size: 40, color: Colors.orange),
                        title: Text(
                          'Masa No: ${orders[index].tableNo} - Ürün: ${orders[index].orderName}',
                          style: TextStyle(fontSize: 18),
                        ),
                        subtitle: Text('Durum: ${orders[index].orderStatus}', style: TextStyle(fontSize: 16)),
                        trailing: orders[index].orderStatus == "Siparis Edildi"
                            ? IconButton(
                                icon: Icon(Icons.check_circle, color: Colors.green, size: 30),
                                onPressed: () {
                                  updateOrderStatus(orders[index].orderId, "Teslim Edildi");
                                },
                              )
                            : Icon(Icons.check_circle, color: Colors.grey), // Teslim edildiğinde gri göster
                      ),
                    );
                  },
                )
              : Center(child: Text('Sipariş Yok', style: TextStyle(fontSize: 20))), // Sipariş yoksa mesaj göster
    );
  }
}


